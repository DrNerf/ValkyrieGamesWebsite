jQuery(document).ready(function () {
    jQuery("#LoginAjaxBtn").click(function () {
        var username = jQuery("#lwa_user_login").val();
        var password = jQuery("#lwa_user_pass").val();
        var rememberMe = jQuery("#lwa_rememberme").val();
        jQuery.post("/Account/LoginAJAX", { UserName: username, Password: password, RememberMe: rememberMe }, function (data) {
            if (data) {
                jQuery.get("/Account/GetLoginPartial", function (data) {
                    jQuery("#LoginPartialContainer").html(data);
                    jQuery(".close").first().trigger("click");
                });
            }
            else {
                new PNotify({
                    title: 'Whoopsay!',
                    text: 'The username or password is wrong ;(',
                    type: 'error'
                });
            }
        });
    });
});