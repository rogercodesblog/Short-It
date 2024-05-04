let resultContainer = $('#resultContainer');
let resultContainerText = $('#resultContainerText');
let resultContainerNewLinkInfo = $('#newLink');
let btnGenerateShortLink = $('#btnGenerate');
let linkFullUrl = $('#linkFullUrl');
let linkShortUrl = $('#linkShortUrl');
let inputUrl;
let generatedShortUrl;
let fullUrlWithGeneratedShortLink;
let apiCallResponse;
let apiCallResponseText;
let apiCallErrorDefaultResponseText = 'There was an error while creating the short link, please try again.';
let txtInputEmptyErrorMessage = 'The provided url can\'t be empty';
const toastNotification = $('#liveToast');

function generateShortLink() {
    inputUrl = $("#txtUrl").val();
    isButtonEnabled(false);

    if (isUrlEmpty(inputUrl)) {
        showResult(false, txtInputEmptyErrorMessage);
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

            generatedShortUrl = response.data.shortLink
            showResult(true, response.message);
        },
        error: function (xhr) {

            if (xhr.status === 400) {

                apiCallResponse = JSON.parse(xhr.responseText);

                for (var key in apiCallResponse) {
                    console.log(`Incorrect value for ${key}: ${apiCallResponse[key]}`);
                    apiCallResponseText = `${apiCallResponse[key]}`;
                }

                showResult(false, apiCallResponseText);
            }
            else if (xhr.status === 500) {
                showResult(false, xhr.responseText);
            }
            else {
                showResult(false, apiCallErrorDefaultResponseText);
            }
        }
    });

}

function showResult(Success, Message) {

    resultContainer.removeClass("d-none").addClass("d-block");

    if (Success) {
        resultContainer.removeClass("alert-danger").addClass("alert-success");
        resultContainerText.text(Message);
        resultContainerNewLinkInfo.removeClass('d-none');

        setFullUrlWithShortLink($(location).attr('href'), generatedShortUrl)

        setLinkValues(inputUrl, fullUrlWithGeneratedShortLink);
    }
    else {
        resultContainer.removeClass("alert-success").addClass("alert-danger");
        resultContainerText.text(Message);
        resultContainerNewLinkInfo.addClass('d-none');
        setLinkValues("", "");
    }
    isButtonEnabled(true);
}

function isUrlEmpty(url) {
    return url == '' ? true : false;
}

function isButtonEnabled(state) {
    btnGenerateShortLink.text(state ? "Short It!" : "Generating...");
    btnGenerateShortLink.prop('disabled', state ? false : true);
}

function setLinkValues(fullLink, shortLink) {
    linkFullUrl.attr("href", fullLink);
    linkFullUrl.text(fullLink);
    linkShortUrl.attr("href", shortLink);
    linkShortUrl.text(shortLink);
}

function copyUrl() {
    copyShortUrlToClipboard(fullUrlWithGeneratedShortLink);

    var toast = new bootstrap.Toast(toastNotification)
    toast.show()
}

function copyShortUrlToClipboard(link) {
    var $temp = $("<input>");
    $("body").append($temp);
    $temp.val(link).select();
    document.execCommand("copy");
    $temp.remove();
}

function setFullUrlWithShortLink(baseurl, shortlink) {
    fullUrlWithGeneratedShortLink = `${baseurl}To/${shortlink}`;
}

function viewStats() {
    window.location.href = `/stats/${generatedShortUrl}`;
}