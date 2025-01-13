// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
/*$(document).ready(function () {
    `<button id="Category" onclick="Category()"`
});*/

/*================================For Register================================*/
function Insert() {
    var form = $(".needs-validation")
    event.preventDefault();
    if (form[0].checkValidity() === false) {
        event.stopPropagation();
    } else {
        Register();
    }
    form.addClass('was-validated');
}
/*function Register() {
    event.preventDefault()
    var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //ini ngambil value dari tiap inputan di form nya
    obj.FirstName = $("#firstname").val();
    obj.LastName = $("#lastname").val();
    obj.PhoneNumber = $("#phone").val();
    obj.BirthDate = $("#birthdate").val();
    obj.Email = $("#email").val();
    obj.Gender = $("#gender").val();
    obj.Password = $("#password").val();
    obj.Role = 3;
    console.log(obj);
    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post

    $.ajax({
        url: "Accounts/register",
        type: "POST",
        *//*headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
        },*//*
        data: obj, //jika terkena 415 unsupported media type (tambahkan headertype Json & JSON.Stringify();)
        //data: JSON.stringify(obj)

    }).done((result) => {
        console.log(result)
        if (result.status == 200) {
            Swal.fire({
                icon: 'success',
                title: 'Register successfully'
            }).then(function () {
                window.location = "login";
            });
        } else {
            Swal.fire({
                icon: 'error',
                title: 'Register Gagal',
                text: result.message,
            })
        }
    }).fail((error) => {
        console.log(error)
        Swal.fire(
            'Register failed',
            'Register error'
        )
    });
}
*/



function Register() {
    event.preventDefault()
    var formData = new FormData();
    formData.append('foto', $('#foto').get(0).files[0]);
    formData.append('firstname', $("#firstname").val());
    formData.append('lastname', $("#lastname").val());
    formData.append('phonenumber', $("#phone").val());
    formData.append('birthdate', $("#birthdate").val());
    formData.append('email', $("#email").val());
    formData.append('gender', $("#gender").val());
    formData.append('password', $("#password").val());
    formData.append('role', 3);
    console.log(formData)
    $.ajax({
        url: "/Accounts/registerphoto",
        type: "POST",
        processData: false,
        contentType: false,
        cache: false,
        data: formData,
        enctype: 'multipart/form-data'
    }).done((result) => {
        console.log(result)
        if (result.status == 200) {
            Swal.fire({
                icon: 'success',
                title: 'Register successfully'
            }).then(function () {
                window.location = "login";
            });
        } else {
            Swal.fire({
                icon: 'error',
                title: 'Register Gagal',
                text: result.message,
            })
        }
    }).fail((error) => {
        console.log(error)
        Swal.fire(
            'Register failed',
            'Register error'
        )
    });
}

/*================================For Login================================*/
function loginUser() {
    event.preventDefault();
    var loginVM = {
        Email: $("#Email").val(),
        Password: $("#Password").val()
    };
    //console.log(logInVM.JWT);
    $.ajax({
        url: '/login/verifylogin',
        type: 'post',
        data: loginVM,
        dataType: "JSON"
        /*headers : {
            "Authorization": "Bearer " + JWToken,
        }*/

    }).done(result => {
        if (result.status == 200) {
            Swal.fire({
                icon: 'success',
                title: 'login success',
                showConfirmButton: false,
                timer: 1500
            }).then(function () {
                if (result.roles == "Student") {
                    window.location = "/"
                } else {
                    window.location = result.roles;
                }
                
            });
        } else {
            Swal.fire({
                icon: 'error',
                title: 'Login Gagal',
                text: result.message,
            })
        }
    }).fail(error => {
        Swal.fire({
            icon: 'error',
            title: 'Login Gagal',
            text: 'Data Tidak Sesuai!',
        });
    });
}

/*================================For Category================================*/


/*================================Course================================*/
Course();
function Course() {
    $.ajax({
        url: "Courses/Getall",
        success: function (result) {
            console.log(result);
            var text = "";
            $.each(result, function (key, val) {
                if (val.status == 3) {
                    var avg = AvgRating(val.id);
                    text +=`<div class="col-md-4">
                                <div class="card my-2">
                                    <a href="/Course/${val.id}" class="stretched-link"></a>
                                    <img class="card-img-top" src="/upload/images/${val.picture}" alt="Card image" style="width:100%">
                                    <div class="card-body">
                                        
                                        <h4 class="card-title" >${val.title}</h4>
                                        <p class="card-text" >${val.features}</p>
                                        <i class="fa fa-star text-warning"></i> <b>${avg.rating} </b> <small class="text-secondary">(${avg.review})</small>
                                        <h5 class="card-text">${"Rp. " + val.price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".")}</h5>
                                    </div>
                                </div>
                            </div>`;
                }
            });
            $("#contentCourse").html(text);
        }
    })
}

function CoursePage(ID) {
    sessionStorage.setItem("CourseId", ID);
    location.assign('/student/ContentLogin');
}


Review();
function Review() {
    $.ajax({
        url: "Reviews/Getall",
        success: function (result) {
            console.log(result);
            var text = "";
            
            $.each(result.slice(0,6), function (key, val) {
                
                    text += `<div class="col-lg-4">
                            <div class="testimonial-item mx-auto mb-5 mb-lg-0">
                            <img class="img-fluid rounded-circle mb-3" src="Upload/images/${val.user.profilePicture}" alt="..." />
                            <h5>${val.user.firstName} ${val.user.lastName}</h5>
                            <p class="font-weight-light mb-0">${val.contents}</p>
                            </div>
                        </div>`
                 
            })
           
            $("#reviewCourses").html(text);

        }
    })
}


function AvgRating(id) {
    var check;
    $.ajax({
        async: false,
        url: "/reviews/AvgRating/" + id,
        success: function (result) {
            check = result;
        }
    })
    return check
}
