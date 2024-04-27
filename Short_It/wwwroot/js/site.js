
function GenerateShortLink() {
    var inputUrl = document.getElementById("txtUrl").value;

    //Todo: Validate if input url is null or not


    var createlink = {
        FullLink: inputUrl
    };

    var json = JSON.stringify(createlink);

    $.ajax({
        type: "POST",
        url: "/Link/CreateShortLink",
        data: json,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            alert('It Worked!');
        },
        error: function(msg) {
            alert('It didn\'t worked :(');
        }
    });

  
}