$("#sign-out").click(function (e) {
 
    $.ajax({
            type: "POST",
            url: "https://localhost:5001/Account/Signout",
            dataType: 'json',
            crossDomain: true,
            success: function(data){
                window.location.href = data;
            }
        });
});
