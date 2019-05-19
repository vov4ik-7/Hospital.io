
function ScheduleButtonSuccess(data) {
    console.log(data);
    $('#dialogContentSchedulePsychologist').html(data);
    $('#modalFormSchedulePsychologist').modal('show');
}

function SignButtonSuccess(data) {
    console.log(data);
    $('#dialogContentSignPsychologist').html(data);
    $('#modalFormSignPsychologist').modal('show');
}

function ViewAllButtonSuccess(data) {
    console.log(data);
    $('#dialogContentViewAllPsychologist').html(data);
    $('#modalFormViewAllPsychologist').modal('show');
}

function AddReportButtonSuccess(data) {
    console.log(data);
    $('#dialogContentAddReport').html(data);
    $('#modalFormAddReport').modal('show');
}


$(document).ready(function() {
    if (sessionStorage.showMessageAfterPageLoad == 'true') {
        toastr.success(sessionStorage.userCreationStatus);
        sessionStorage.showMessageAfterPageLoad = false;
    }
});

function SuccessCreate(data) {
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

function SuccessEdit(data) {
    $('#modalFormEditPsychologist').modal('hide');

    if (data.status == "success") {
        sessionStorage.showMessageAfterPageLoad = true;
        sessionStorage.userCreationStatus = data.description;
        location.reload(true);
    }
    else if (data.status == "error") {
        console.log(data.description);
        toastr.error(data.description);
    }
}

function SuccessSign(data) {
    $('#modalFormSignPsychologist').modal('hide');

        if (data.status == "success") {
            sessionStorage.showMessageAfterPageLoad = true;
            sessionStorage.userCreationStatus = data.description;
            location.reload(true);
        }
        else if (data.status == "error") {
            console.log(data.description);
            toastr.error(data.description);
        }
}

function SuccessAddedReport(data) {
    $('#modalFormAddReport').modal('hide');

        if (data.status == "success") {
            sessionStorage.showMessageAfterPageLoad = true;
            sessionStorage.userCreationStatus = data.description;
            location.reload(true);
        }
        else if (data.status == "error") {
            console.log(data.description);
            toastr.error(data.description);
        }
}