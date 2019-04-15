$("#sign-in").click(function (e) {
 
    console.log("keeek");
    $.get("https://localhost:5001/Account/Signin").then(data => {
        console.log(data);
        $('#dialogContent').html(data);
        $('#elegantModalForm').modal('show');
    });
});
