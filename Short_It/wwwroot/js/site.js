let labelResult = $('#labelResult');
let btnGenerateShortLink = $('#btnGenerate');
let inputUrl;
let ApicallResponse;
let ApicallResponseText;
let ApiCallErrorDefaultResponseText = 'There was an error while creating the short link, please try again.';
let txtInputEmptyErrorMessage = 'The provided url can\'t be empty';

function GenerateShortLink() {
    inputUrl = $("#txtUrl").val();
    isButtonEnabled(false);

    if (IsUrlEmpty(inputUrl)) {
        ShowResult(false, txtInputEmptyErrorMessage);
        return;
    }

    $.ajax({
        url: '/Link/CreateShortLink',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        datatype: 'JSON',
        data:
            JSON.stringify({
                'FullLink': inputUrl
            }),
        success: function (response) {

            //TODO: Add a better way to see the "new short link"

            ShowResult(true, `${response.message} \n Short Link: ${response.data.shortLink}` );

        },
        error: function (xhr) {

            if (xhr.status === 400) {

                ApicallResponse = JSON.parse(xhr.responseText);

                for (var key in ApicallResponse) {
                    console.log(`Incorrect value for ${key}: ${ApicallResponse[key]}`);
                    ApicallResponseText = `${ApicallResponse[key]}`;
                }

                ShowResult(false, ApicallResponseText);
            }
            else if (xhr.status === 500) {
                ShowResult(false, xhr.responseText);
            }
            else {
                ShowResult(false, ApiCallErrorDefaultResponseText);
            }
        }
    });

}

function ShowResult(Success, Message) {
    if (Success) {
        labelResult.text(Message);
        labelResult.removeClass("d-none").addClass("d-block").addClass("alert-success");
    }
    else {
        labelResult.text(Message);
        labelResult.removeClass("d-none").addClass("d-block").addClass("alert-danger");
    }
    isButtonEnabled(true);
}

function IsUrlEmpty(url) {
    return url == '' ? true : false;
}

function isButtonEnabled(state) {
    //Todo: Change text to loading... or put a spinner
    btnGenerateShortLink.prop('disabled', state ? false : true);
}