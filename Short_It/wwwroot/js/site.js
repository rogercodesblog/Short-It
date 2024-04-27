let labelResult = $('#labelResult');
let btnGenerateShortLink = $('#btnGenerate');
let inputUrl;
let ApicallResponseErrors;
let ApicallResponseErrorsText;
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
        success: function (textStatus) {
            //Todo: Show correct message and new link
            ShowResult(true, textStatus);
        },
        error: function (xhr) {

            if (xhr.status === 400) {

                ApicallResponseErrors = JSON.parse(xhr.responseText);

                for (var key in ApicallResponseErrors) {
                    console.log(`Incorrect value for ${key}: ${ApicallResponseErrors[key]}`);
                    ApicallResponseErrorsText = `${ApicallResponseErrors[key]}`;
                }

                ShowResult(false, ApicallResponseErrorsText);
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
    btnGenerateShortLink.prop('disabled', state ? false : true);
}