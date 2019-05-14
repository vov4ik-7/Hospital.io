
$("#create-appointment").click(function (e) {
 
    $.get(urls.createAppointment).then(data => {
        console.log(data);
        $('#createAppDialog').html(data);
        $('#createAppModalForm').modal('show');

        //$(".datepicker").datepicker();
        $('#sPicker').datetimepicker({
        });
        $('#ePicker').datetimepicker({
            useCurrent: false,
        });
        $("#sPicker").on("change.datetimepicker", function (e) {
            $('#ePicker').datetimepicker('minDate', e.date);
        });
        $("#ePicker").on("change.datetimepicker", function (e) {
            $('#sPicker').datetimepicker('maxDate', e.date);
        });
    });
});

$(document).ready(function() {
    if (sessionStorage.showMessageAfterPageLoad == 'true') {
        toastr.success(sessionStorage.userCreationStatus);
        sessionStorage.showMessageAfterPageLoad = false;
    }
});

function SuccessCreateAppointment(data) {
    $('#createAppModalForm').modal('hide');

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