
$(document).ready(function () {
    CertificateData();
});


function CertificateData() {
    var enrollmentId = window.location.pathname.substring(window.location.pathname.lastIndexOf('/') + 1)
    $.ajax({
        url: "/users/get/" + userId,
        success: function (result) {
            console.log(result);
            var userName = "";
            userName = `${result.firstName} ${result.lastName}`;
            $("#userName").html(userName);
        }
    }).then(function (response3) {
        $.ajax({
            url: "/enrollment/get/" + enrollmentId,
            success: function (result) {
                console.log(result);
                var courseName = "";
                courseName = `${result.course.title}`;
                $("#courseName").html(courseName);
                var enrollmentEnd = "";
                enrollmentEnd = `${$.datepicker.formatDate('dd MM yy', new Date(result.endDate))}`;
                $("#enrollmentEnd").html(enrollmentEnd);
                $.ajax({
                    url: "/users/get/" + result.course.userId,
                    success: function (result2) {
                        console.log(result2);
                        var instructorName = "";
                        instructorName = `${result2.firstName} ${result2.lastName}`;
                        $("#instructorName").html(instructorName);
                    }
                }).then(function (response3) {
                    window.print()
                });
            }
        })
    });
    
}


window.onafterprint = back;
function back() {
    window.history.back();
}