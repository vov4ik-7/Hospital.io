$("#sign-in").click(function (e) {

    $.get(urls.signin).then(data => {
        console.log(data);
        $('#dialogContent').html(data);
        $('#elegantModalForm').modal('show');
    });
});
