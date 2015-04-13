var $ = new jQuery.noConflict();

function formatDate(jsonDate) {
    var date = new Date(parseInt(jsonDate.substr(6)));
    return (date.getMonth() + 1) + "/" + date.getDate() + "/" + date.getFullYear();
}

$(document).ready(function () {
    $.get("/Home/GetFooterRecords?count=5", function (data) {
        console.log(data);
        $("#TopicsFooterTemplate").tmpl(data.LatestTopics).appendTo("#TopicsFooter");
        $("#PostsFooterTemplate").tmpl(data.LatestPosts).appendTo("#PostsFooter");
        $("#NewsFooterTemplate").tmpl(data.LatestNews).appendTo("#NewsFooter");
    });
});