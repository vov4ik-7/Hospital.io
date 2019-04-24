$("#sign-out").click(function (e) {
 
    $.ajax({
            type: "POST",
            url: urls.signout,
            dataType: 'json',
            crossDomain: true,
            success: function(data){
                window.location.href = data;
            }
        });
});
