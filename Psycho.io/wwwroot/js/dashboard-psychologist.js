
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

function OnCalendarEventOpen(event) {
    $.ajax({
        url: $('#doctorUtils').attr('show-app-info-url'),
        data: { appointmentId: event.id },
        method: "GET",
        success: function (data) {
            $('#showAppDialog').html(data);
            $('#showAppResult').modal('show');
        }
    });
}

function AddAnalysisPopupSuccess(data) {
    $('#addAnalysisDialog').html(data);
    $('#addAnalysisResult').modal('show');
    $('#onSubmitAddAnalisys').click(onSubmitAddAnalysis);
}

function FinishAppointmentPopupSuccess(data) {
    $('#finishAppointmentDialog').html(data);
    $('#finishAppointmentResult').modal('show');
}

function onSubmitAddAnalysis(event) {
    event.preventDefault();
    if (!$('#addAnalysisForm')[0].checkValidity()) {
        return;
    }

    var data = $('#addAnalysisForm').serializeArray();
    var file = $('#analysisFile')[0].files[0];
    var formData = new FormData();
    $.each(data, (i, v) => formData.append(v.name, v.value));
    formData.append("File", file);
    $.ajax({
        method: "POST",
        url: $('#doctorUtils').attr('show-add-analysis-popup-url'),
        data: formData,
        processData: false,
        contentType: false,
        success: SuccessAddAnalysis
    });
}

function SuccessAddAnalysis(data) {
    $('#addAnalysisResult').modal('hide');
    addAnalysisRow(data);
}

function addAnalysisRow(newAnalysis) {
    var row = `<tr id="analysis-${newAnalysis.id}">` +
        `<td>${newAnalysis.name}</td>` +
        `<td>${newAnalysis.analysisResult}</td>` +
        `<td>${newAnalysis.doctorConclusion}</td>` +
        `<td><a onclick="downloadAnalysis('${newAnalysis.id}')" class="btn btn-info btn-rounded btn-sm my-0">Download</a></td>` +
        `<td><a onclick="removeAnalysis('${newAnalysis.id}')" class="far fa-trash-alt fa-lg"></a></td>` +
        '</tr>';

    $("#analyses-content").append(row);
}

function removeAnalysis(analysisId) {
    $.ajax({
        method: "POST",
        url: $('#doctorUtils').attr('remove-analysis-url'),
        data: { analysisId: analysisId },
        success: function (data) {
            $(`#analysis-${data}`).remove();
        }
    });
}

function downloadAnalysis(analysisId) {
    window.location = $('#doctorUtils').attr('download-analysis-url') + `?analysisId=${analysisId}`;
}

function finishAppointment() {
    var selectedServiceIds = [];
    var selectedServicesJquery = $("#selectedServices").children();
    $.each(selectedServicesJquery, function (index, elem) {
        selectedServiceIds.push(parseInt($(elem).attr("service-id")));
    });

    var appointmentId = parseInt($("#finishAppointmentId").val());
    $.ajax({
        method: "POST",
        url: $('#doctorUtils').attr('finish-appointment-url'),
        data: { appointmentId, selectedServiceIds },
        success: function () {
            $('#finishAppointmentResult').modal('hide');
            $('#appointmentStatusContainer').empty();
            $('#appointmentStatusContainer').html('<span class="badge badge-danger">Finished</span>');
        }
    });
}
