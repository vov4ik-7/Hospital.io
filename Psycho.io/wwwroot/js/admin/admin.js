
$("#create-psychologist").click(function (e) {
 
    $.get(urls.createPsychologist).then(data => {
        console.log(data);
        $('#dialogContentUp').html(data);
        $('#darkModalForm').modal('show');

        //$(".datepicker").datepicker();

        /*$('.datepicker').pickadate({
            formatSubmit: 'dd/mm/yyyy',
        });
        $('.datepicker').removeAttr('readonly');

        $('.datepicker').on('change', function() {
            let dateValue = $(this).val();
            console.log(dateValue);
        });*/

    });
});

$(document).ready(function() {
    if (sessionStorage.showMessageAfterPageLoad == 'true') {
        toastr.success(sessionStorage.userCreationStatus);
        sessionStorage.showMessageAfterPageLoad = false;
    }
});

function Success(data) {
    $('#darkModalForm').modal('hide');

    if (data.status == "success") {
        //toastr.success(data.description);
        sessionStorage.showMessageAfterPageLoad = true;
        sessionStorage.userCreationStatus = data.description;
        location.reload(true);
    }
    else if (data.status == "error") {
        console.log(data.description);
        toastr.error(data.description);
    }
}
