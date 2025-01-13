/* table data read and population */
$(document).ready(function () {
    let courseId = sessionStorage.getItem("CourseId");
    $.ajax({
        url: `/courses/getmasterdata/${courseId}`,
        success: function (results) {
            console.log(results)
            let result = results.result;
            console.log("courses", result);

            //course heading
            var coursePict = `<img src="/Upload/images/${result.picture}" class="mx-auto d-block" style="width:70%">`
            $('#coursePicture').html(coursePict);
            $('.course-title').html(result.title);
            $('.course-description').html(result.description);
            var categories = "";
            for (var i = 0, length = result.categories.length; i < length; i++) {
                if (i == length - 1) {
                    categories += `<span class="badge badge-info">${result.categories[i].name}</span>`
                }
                else {
                    categories += `<span class="badge badge-info">${result.categories[i].name}</span> `;
                }
            }
            $('.course-category').html(categories);
            //feedback
            $('#feedback').val(result.feedback);
            switch (result.status) {
                case "Incomplete":
                    $('.course-status').html(`<span class="badge badge-secondary">${result.status}</span> `);
                    break;
                case "Review":
                    $('.course-status').html(`<span class="badge badge-primary">${result.status}</span> `);
                    break;
                case "Revise":
                    $('.course-status').html(`<span class="badge badge-warning">${result.status}</span> `);
                    break;
                case "Approve":
                    $('.course-status').html(`<span class="badge badge-success">${result.status}</span> `);
                    $('#reviewTab').remove();
                    $('#approveButton').remove();
                    $('#reviewButton').remove();
                    break;
            }
            
            $('.course-price').html(`${toRp(result.price)}`);

            //creator profile
            $('#firstName').html(`: ${result.user.firstName}`);
            $('#lastName').html(`: ${result.user.lastName}`);
            $('#birthDate').html(`: ${result.user.birthDate.toString().substring(0, 10)}`);
            switch (result.user.gender) {
                case 0:
                    $('#gender').html(`: Laki-laki`);
                    break;
                case 1:
                    $('#gender').html(`: Perempuan`);
                    break;
            }
            $('#phone').html(`: ${result.user.phoneNumber}`);
            $('#email').html(`: ${result.user.email}`);

            //course contents
            let text = "";
            for (let i = 0; i < result.contents.length; i++) {
                text += `<div class="card">
                            <div class="card-header" id="heading${i}">
                                <h5 class="mb-0">
                                    <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapse${i}" aria-expanded="false" aria-controls="collapse${i}">
                                        ${result.contents[i].title}
                                    </button>
                                </h5>
                            </div>
                            <div id="collapse${i}" class="collapse show" aria-labelledby="heading${i}" data-parent="#courseContents">
                                <div class="card-body">
                                    <div id="subContents${i}">`;
                for (var j = 0, title = "", videoName = ""; j < result.contents[i].subContent.length; ) {
                    var subContent = result.contents[i].subContent[j++];
                    title = subContent.title;
                    console.log(title);
                    videoName = subContent.videoName;
                    console.log(videoName);
                    //if (i == len - 1) {
                    //    title += `${result.contents[i].subContent[j].title}`;
                    //}
                    //else {
                    //    text += `${result.contents[i].subContent[j].title}<br>`;
                    //}
                    text += `<div class="card">
                                <div class="card-header" id="subHeading${i}">
                                    <h5 class="mb-0">
                                        <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#subCollapse${i}" aria-expanded="false" aria-controls="subCollapse${i}">
                                            ${title}
                                        </button>
                                    </h5>
                                </div>
                                <div id="subCollapse${i}" class="collapse show" aria-labelledby="subHeading${i}" data-parent="#subContents${i}">
                                    <div class="card-body">
                                        <div class="row">
                                            <div class = "col-8">
                                                <h5><b>Video</b></h5>
                                                <hr>
                                                <video class="mx-auto d-block" style="width:100%" controls>
                                                    <source src="/upload/videos/${videoName}">
                                                </video>
                                            </div>
                                            <div class = "col-4">
                                                <h5><b>Resources</b></h5>
                                                <hr>`;
                    for (var k = 0, fileName = ""; k < subContent.resources.length; ) {
                        resource = subContent.resources[k++];
                        fileName = resource.fileName;
                        console.log(fileName);
                        text += `<a href = "/upload/resources/${fileName}" target = "_blank" > ${fileName} </a><br>`
                    }
                    text += `</div></div><br>`;

                    //quizzes
                    text += `<h5 style="text-align: center"><b>Quizzes</b></h5>
                            <hr>`;
                    for (var l = 0; l < subContent.quizzes.length;) {
                        quiz = subContent.quizzes[l++];
                        text += `<p>${l.toString() + '. ' + quiz.question}</p>`;
                        for (var m = 0; m < quiz.answers.length;) {
                            answer = quiz.answers[m++];
                            text += `<p style = "display:inline-block;"> ${answer.contents} </p>`;
                            if (answer.isCorrect) {
                                text += '<i class="fa fa-check text-success"></i>'
                            }
                            if (m != quiz.answers.length) {
                                text += '<br>';
                            }
                        }
                    }

                    text += `</div></div></div></div>`;
                }
                text += `</div></div></div></div>`;    
            }
            $('#courseContents').html(text);
        
        }
    });
    
});

function toRp(nominal) {
    let sal = nominal.toString();
    let salLength = sal.length;
    if (salLength > 3) {
        var newSal = '';
        if (salLength % 3 != 0) {
            newSal = sal.substring(0, salLength % 3);
        }
        else {
            newSal = sal.substring(0, 3)
        }
        for (var i = newSal.length; i < salLength; i += 3) {
            newSal += '.' + sal.substring(i, i + 3);
        }
        sal = newSal;
    }
    return "Rp" + sal + ",00";
}

function Approve() {
    let courseId = sessionStorage.getItem("CourseId");
    console.log(courseId);
    var result;
    $.ajax({
        url: `/courses/get/${courseId}`,
        success: function (results) {
            result = results;
        }
    });

    Swal.fire({
        title: `Do you want to approve this course?`,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, approve it.'
    }).then((answer) => {
        if (answer.isConfirmed) {
            console.log(result.status);
            result.status = 3;
            $.ajax({
                url: `/courses/put`,
                type: "PUT",
                data: result,
                dataType: "json"
            }).done((statusCode) => {
                if (statusCode == 200) {
                    Swal.fire({
                        title: "Approve Course Successful", text: `The course with ID ${courseId} has been approved`,
                        type: "success"
                    }).then(function () {
                        location.reload();
                    }
                    );
                }
                else {
                    Swal.fire({
                        title: "Approve failed", text: results.message,
                        type: "failed"
                    }).then(function () { location.reload() });
                }
            }).fail((error) => {
                Swal.fire({
                    title: "Approve Failed", text: "Something went wrong!",
                    type: "failed"
                }).then(function () { location.reload() });
            });
        }
    })
}

function Revise() {
    event.preventDefault();
    let courseId = sessionStorage.getItem("CourseId");
    var result;
    $.ajax({
        url: `/courses/get/${courseId}`,
        success: function (results) {
            result = results;
        }
    });
    Swal.fire({
        title: `Do you want to send this feedback to revise the course?`,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, send it.'
    }).then((answer) => {
        if (answer.isConfirmed) {
            result.status = 2;
            result.feedback = $("#feedback").val();
            $.ajax({
                url: `/courses/put`,
                type: "PUT",
                data: result,
                dataType: "json"
            }).done((statusCode) => {
                if (statusCode == 200) {
                    Swal.fire({
                        title: "Feedback to revise has been sent", text: `The course with ID ${courseId} is to be revised: ${result.feedback}`,
                        type: "success"
                    }).then(function () {
                        location.reload();
                    }
                    );
                }
                else {
                    console.log(statusCode);
                    Swal.fire({
                        title: "Send feedback failed", text: results.message,
                        type: "failed"
                    }).then(function () { location.reload() });
                }
            }).fail((error) => {
                Swal.fire({
                    title: "Failed", text: "Something went wrong!",
                    type: "failed"
                }).then(function () { location.reload() });
            });
        }
    })
}