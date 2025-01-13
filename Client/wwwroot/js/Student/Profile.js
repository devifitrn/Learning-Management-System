$(document).ready(function () {
    detailProfile();
    console.log(userId);
    detailCourse();
});

function detailProfile() {
    $.ajax({
        url: "/users/get/" + userId,
        success: function (result) {
            console.log(result)
            var result = result;
            switch (result.status) {
                case 0:
                    $("#instructorButton").attr("class", "btn btn-sm btn-warning");
                    $("#instructorButton").attr("onClick", "SendRequest()");
                    $("#instructorButton").html("Send Request to be an Instructor");
                    break;
                case 1:
                    $("#instructorButton").removeAttr("onClick").attr("class", "btn btn-sm btn-info");
                    $("#instructorButton").html("Instructor Sign Up request is to be approved");
                    break;
                case 2:
                    $("#instructorButton").removeAttr("onClick").attr("class", "btn btn-sm btn-success");
                    $("#instructorButton").html("You are an instructor");
                    break;
            }

            // Table Course 
            var pictureProfile = "";
            if (result.profilePicture == null) {
                pictureProfile += `<img class="img-account-profile rounded-circle mb-2" width="182" 
                src="http://assets.kompasiana.com/items/album/2021/03/24/blank-profile-picture-973460-1280-605aadc08ede4874e1153a12.png?t=o&v=770" alt="">`
                pictureProfile += `<input class="form-control" type="file" id="foto" accept="image/*" required />
                    <div class="invalid-feedback"> Harap Input Picture</div>
                    <!-- Profile picture upload button-->
                    <button class="btn btn-sm btn-primary mt-3" type="button" onclick="UpdatePicture()">Upload</button>`
            } else {
                pictureProfile += `<img src="/Upload/images/${result.profilePicture}" class="mx-auto d-block" style="width:100%">`
                pictureProfile += `<input class="form-control" type="file" id="foto" accept="image/*" required />
                    <div class="invalid-feedback"> Harap Input Picture</div>
                    <!-- Profile picture upload button-->
                    <button class="btn btn-sm btn-primary mt-3" type="button" onclick="Update()">Upload</button>`
            }

            $("#picture-profile").html(pictureProfile);
            var detailProfile = "";
            detailProfile += `<div class="row mb-3">
                             <div class="col-md-6">
                                <label class="small mb-1" for="inputFullName" id="fullName">First name</label>
                                    <input class="form-control" id="inputFullName" type="text" placeholder="Enter your fullname" value=${result.firstName}>
                            </div>`
            detailProfile += `  <div class="col-md-6">
                                <label class="small mb-1" for="inputFullName" id="fullName">Last name</label>
                                <input class="form-control" id="inputFullName" type="text" placeholder="Enter your fullname" value=${result.lastName}>
                        </div>
                        </div>`
            detailProfile += `<div class="mb-3">
                            <label class="small mb-1" for="inputUsername" id="email">Email Address</label>
                            <input class="form-control" id="inputUsername" type="email" placeholder="Enter your email" value=${result.email}>
                        </div>`

            detailProfile += `<div class="row gx-3 mb-3">
                            <div class="col-md-6">
                                <label class="small mb-1" for="inputPhone" id="phoneNumber">Phone number</label>
                                <input class="form-control" id="inputPhone" type="number" placeholder="Enter your phone number" value=${result.phoneNumber}>
                            </div>
                            <div class="col-md-6">
                                <label class="small mb-1" for="inputBirthday" id="birthDate">Birthdate</label>
                                <input class="form-control" id="inputBirthday" type="date" name="birthdate" placeholder="Enter your birthday" value=${$.datepicker.formatDate('yy-mm-dd', new Date(result.birthDate))}>
                            </div>
                        </div>`
            detailProfile += `<div class="text-center"><button class="btn btn-sm btn-primary text-center" type="button" onclick="Update()">Save changes</button></div>`
            $("#profile-detail").html(detailProfile);

        }
    })
}

function Update(userId) {

    event.preventDefault()
    // var nik = $("#nik").val();
    var obj = new Object();
    obj.Email = $("#email").val();
    obj.fullname = $("#fullName").val();
    obj.phoneNumber = $("#phoneNumber").val();
    obj.birthdate = $("#birthDate").val();
    obj.picture = $("#picture-profile").val();
    $.ajax({
        url: "/Users/updateId/" + userId,
        type: "PUT",
        data: obj
        /*headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
        },
        dataType: 'json', //jika terkena 415 unsupported media type (tambahkan headertype Json & JSON.Stringify();)
        data: JSON.stringify(obj)*/

    }).done((result) => {
        Swal.fire(
            'success',
            'success'
        )
    }).fail((error) => {
        Swal.fire(
            'failed',
            'error'
        )
    });
}

function detailCourse() {
    $.ajax({
        url: "/enrollment/Getenrollmentdata/" + userId,
        success: function (result) {
            console.log(result)
            var result = result.result;
            console.log(result)
            // Table Course 
            var detailCourse = "";
            var pictureCourse = "";
            detailCourse += `<thead class="thead-dark">
                                <tr>
                                    <th width="3%">Id</th>
                                    <th width="22%">Title</th>
                                    <th width="15%">StartDate</th>
                                    <th width="15%">EndDate</th>
                                    <th width="15%">Payment Status</th>
                                    <th width="15%">Course Status</th>
                                    <th width="15%">Action</th>
                                </tr>
                            </thead><tbody>`
            $.each(result, function (key, val2) {

                detailCourse += `<tr><td>${val2.id}</td><td>${val2.course.title}</td><td>${$.datepicker.formatDate('dd MM yy', new Date(val2.startDate))}</td>`
                if ($.datepicker.formatDate('yy', new Date(val2.endDate)) == 1) {
                    detailCourse += `<td>-</td>`

                } else {
                    detailCourse += `<td>${$.datepicker.formatDate('dd MM yy', new Date(val2.endDate))}</td>`
                }
                if (val2.status == 2) {
                    detailCourse += `<td>Lunas</td>`
                    if (val2.isComplete) {
                        detailCourse += `<td>Selesai</td>`
                    } else {
                        detailCourse += `<td>Belum Selesai</td>`
                    }
                } else if (val2.status == 1) {
                    detailCourse += `<td>Batal</td>`
                    detailCourse += `<td>-</td>`
                } else {
                    detailCourse += `<td>Belum Lunas</td>`
                    detailCourse += `<td>-</td>`
                }
                if (val2.status == 2) {
                    detailCourse += `<td><a class="btn py-1 btn-primary btn-sm" href="/Student/CourseView/${val2.id}">Start</a> `
                    if (val2.isComplete) {
                        var rev = CheckReview(val2.courseId);
                        if (rev == null) {
                            detailCourse += `<button type="button" class="btn btn-sm btn-warning py-1" onClick="InsertReviewModal('${val2.courseId}')">Ulasan</button></td></tr> `
                        } else {
                            /* detailCourse += `<button type="button" class="btn btn-sm btn-warning py-1" onClick="UpdateReviewModal('${rev.id}')">Ulasan</button> `*/
                            detailCourse += `<a class="btn py-1 btn-success btn-sm" href="/Student/certificate/${val2.id}">Cert</a></td></tr>`
                        }
                    }
                } else {
                    /*detailCourse += `<td><a class="btn btn-info btn-sm" onclick ="StatusBayar();">View</a></td></tr>`*/
                    detailCourse += `<td>-</td></tr>`
                }

                /*detailCourse += `<tr><th>Id</th><td>:</td><td>${val2.course.id}</td></tr>`
                detailCourse += `<tr><th>Title</th><td>:</td><td class="py-0">${val2.course.title}</td></tr>`
                detailCourse += `<tr><th>Features</th><td>:</td><td class="py-0">${val2.course.features}</td></tr>`
                detailCourse += `<tr><th>StartDate</th><td>:</td><td class="py-0">${val2.startDate}</td></tr>`
                detailCourse += `<tr><th>IsComplete</th><td>:</td><td class="py-0">${val2.isComplete}</td></tr>`
                
                detailCourse += `</tbody>`
                
                pictureCourse += `<img src="/assets/img/${val2.course.picture}" class="mx-auto d-block" style="width:100%">`*/
            })
            detailCourse += `</tbody>`
            $("#table-course").html(detailCourse);
            $("#picture-course").html(pictureCourse);

        }
    })
}


function StatusBayar() {
    event.preventDefault();
    $.ajax({
        url: "/enrollment/Getenrollmentdata/" + userId,
        success: function (result) {
            console.log(result)
            var result = result.result;
            $.each(result, function (key, val2) {
                if (val2.status == 1) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Status Pembayaran Anda Pending!',
                    })
                } else if (val2.status == 0) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Status Pembayaran Anda dibatalkan!',
                        footer: '<a href="">ingin melakukan pembelian lagi?</a>'
                    })
                } else {
                    Swal.fire(
                        'error',
                        'question'
                    )
                }

            })
        }
    });
}

function SendRequest() {
    Swal.fire({
        title: `Do you want to send request to be an Instructor?`,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, send that request!'
    }).then((answer) => {
        if (answer.isConfirmed) {
            var student = {
                Id: userId,
                Status: 1
            }
            $.ajax({
                url: "/Users/InstructorSignup",
                type: "PUT",
                dataType: "JSON",
                data: student
            }).done((results) => {
                if (results.status == 200) {
                    Swal.fire({
                        title: `Your request is to be approved`,
                        type: "success"
                    }).then(function () {
                        location.reload();
                    }
                    );
                }
                else {
                    console.log(statusCode);
                    Swal.fire({
                        title: "Request failed to be sent",
                        text: results.message,
                        type: "failed"
                    }).then(function () { location.reload() });
                }
            }).fail((error) => {
                Swal.fire({
                    title: "Failed", text: error.message,
                    type: "failed"
                }).then(function () { location.reload() });
            });
        }
    })

    //event.preventDefault();
    //$.ajax({
    //    url: `/Users/getmasterdata/${userId}`,
    //    success: function (result) {
    //        console.log(result)
    //        var result = result.result;
    //    }
    //});
    //Swal.fire({
    //    title: `Do you want to join the Instructor?`,
    //    icon: 'warning',
    //    showCancelButton: true,
    //    confirmButtonColor: '#3085d6',
    //    cancelButtonColor: '#d33',
    //    confirmButtonText: 'Yes, send it.'
    //}).then((answer) => {
    //    if (answer.isConfirmed) {
    //        result.status = 1;
    //        result.feedback = $("#feedback").val();
    //        $.ajax({
    //            url: `/courses/put`,
    //            type: "PUT",
    //            data: result,
    //            dataType: "json"
    //        }).done((statusCode) => {
    //            if (statusCode == 200) {
    //                Swal.fire({
    //                    title: "Feedback to revise has been sent", text: `The course with ID ${courseId} is to be revised: ${result.feedback}`,
    //                    type: "success"
    //                }).then(function () {
    //                    location.reload();
    //                }
    //                );
    //            }
    //            else {
    //                console.log(statusCode);
    //                Swal.fire({
    //                    title: "Send feedback failed", text: results.message,
    //                    type: "failed"
    //                }).then(function () { location.reload() });
    //            }
    //        }).fail((error) => {
    //            Swal.fire({
    //                title: "Failed", text: "Something went wrong!",
    //                type: "failed"
    //            }).then(function () { location.reload() });
    //        });
    //    }
    //})
}





function CheckReview(id) {
    var check;
    $.ajax({
        async: false,
        url: `/reviews/CheckReview/${userId}/${id}`,
        success: function (result) {
            check = result;
        }
    })
    return check
}
function InsertReviewModal(courseId) {
    $('#reviewModal').modal('show');
    $("#courseId").val(courseId);
}
function SubmitReview() {
    event.preventDefault();
    var rating = $(":checked").val();
    Swal.fire({
        title: `Do you want to submit the review now?`,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, submit it!'
    }).then((answer) => {
        if (answer.isConfirmed) {
            let date = new Date();
            var currentDateTime = (new Date(new Date().getTime() - (new Date().getTimezoneOffset() * 60 * 1000))).toISOString();
            console.log(currentDateTime);

            var review = {
                UserId: userId,
                CourseId: $('#courseId').val(),
                Contents: $('#reviewComment').val(),
                Rating: $(':checked').val(),
                ReviewDate: currentDateTime
            }
            console.log(review);
            $.ajax({
                url: "/reviews/postreview",
                type: "POST",
                dataType: "JSON",
                data: review
            }).done((statusCode) => {
                console.log(statusCode);
                if (statusCode == 200) {
                    Swal.fire({
                        title: `Your review has been submitted`,
                        type: "success"
                    }).then(function () {
                        location.reload();
                    }
                    );
                }
                else {
                    Swal.fire({
                        title: "Review failed to be sent",
                        type: "failed"
                    }).then(function () { location.reload() });
                }
            }).fail((error) => {
                Swal.fire({
                    title: "Failed",
                    text: error.message,
                    type: "failed"
                }).then(function () { location.reload() });
            });
        }
    })
}


function UpdateReviewModal(courseId) {
    $('#UpdateReviewModal').modal('show');
    $("#reviewId").val(reviewId);
}