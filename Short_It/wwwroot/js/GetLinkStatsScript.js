function ViewStats() {
    window.location.href = `/stats/${$("#txtShortUrl").val()}`;
}

function ViewStatsFromHomePage() {

    //Get text box input value
    let inputValue = $("#txtShortUrl").val();

    //This means the user entered the whole url,
    //we'll have to split it to get the short url
    if (inputValue.startsWith("https://") || inputValue.startsWith("http://")) {

        //Split the base url (index 0) and the short link id (index 1)
        var values = inputValue.split("/to/");

        //Select the short link id
        var Url = values[1];

        //Redirect to the page
        window.location.href = `/stats/${Url}`;
        return;
    }
    //This menas the user entered the short url or an empty string
    //Either way, we should redirect to the stats page
    window.location.href = `/stats/${inputValue}`;
}