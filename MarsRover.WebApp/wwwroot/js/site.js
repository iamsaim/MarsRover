
function DeployRover() {

    var obj = new Object();
    obj.plateauId = $("#plateauId").val();
    obj.name = $("#name").val();
    obj.x = $("#x").val();
    obj.y = $("#y").val();
    var width = $("#width").val();
    var height = $("#Height").val();
    obj.CardinalPoint = $("#CardinalPoint").val();
    if (parseInt(obj.x) < 1 || parseInt(obj.x) > parseInt(width)) {
        alert("Cannot deploy rover beyond plateau's width");
        return false;
    }

    if (parseInt(obj.y) < 1 || parseInt(obj.y) > parseInt(height)) {
        alert("Cannot deploy rover beyond plateau's height");
        return false;
    }
    console.log(obj);

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: '/Home/DeployRover',
        data: JSON.stringify(obj),
        async: true,
        success: function (data) {
            window.location.href = "/Home/Index?PlateauId="+data;
        },
        error: function (jqXHR, textStatus, errorThrown) {
            
            alert("Failed to deploy, please fill all the information")
        }
    });
}

function MoveRover(id) {
    $("#roverId").val(id);
    document.getElementById("myModal").style.display = "block";
}

function closePopup () {
    document.getElementById("myModal").style.display = "none";
}

window.onclick = function (event) {
    debugger;
    if (event.target == document.getElementById("myModal") || event.target == document.getElementById("uploadcsvmodel")) {
        closePopup();
        closePopupCSV();
    }
}
function SendMoveRover() {
    var obj = new Object();
    obj.id = $("#roverId").val();
    obj.moves = $("#moves").val();
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: '/Home/MoveRover',
        data: JSON.stringify(obj),
        async: true,
        success: function (data) {
            window.location.href = "/Home/Index?PlateauId=" + $("#plateauId").val();;
        },
        error: function (jqXHR, textStatus, errorThrown) {

            alert("Failed to move rover")
        }
    });
}
function OpenPopupCSV() {
    document.getElementById("uploadcsvmodel").style.display = "block";
}
function closePopupCSV() {
    document.getElementById("uploadcsvmodel").style.display = "none";
}

function uploadcsv() {
    debugger;
    var file = document.getElementById("fileupload").files[0];
    let formData = new FormData();
    formData.append("file", file);
    formData.append("plateauId", $("#plateauId").val());
    $.ajax({
        type: "POST",
        url: '/Home/UploadCSV',
        processData: false,
        mimeType: "multipart/form-data",
        contentType: false,
        data: formData,
        async: true,
        success: function (data) {
            window.location.href = "/Home/Index?PlateauId=" + $("#plateauId").val();
        },
        error: function (jqXHR, textStatus, errorThrown) {

            alert("Invalid file")
        }
    });
}