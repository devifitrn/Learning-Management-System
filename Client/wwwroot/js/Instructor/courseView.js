$(document).ready(function () {
    detailCourse();
    FormCategory()
});

/*============================ Course View ============================*/
function detailCourse() {
    var url = window.location.pathname
    var id = url.substring(url.lastIndexOf('/') + 1)
    $.ajax({
        url: "/courses/GetMasterData/" + id,
        success: function (result) {
            console.log(result)
            var result = result.result;
            // Table Course 
            var detailCourse = "";
            detailCourse += `<tbody><tr><th width="20%">Title</th><td width="3%">:</td><td>${result.title}</td></tr>`
            detailCourse += `<tr><th>Description</th><td>:</td><td>${result.description}</td></tr>`
            detailCourse += `<tr><th>Features</th><td>:</td><td>${result.features}</td></tr>`
            detailCourse += `<tr><th>Price</th><td>:</td><td>${result.price}</td>/tr>`
            detailCourse += `<tr><th>Category</th><td>:</td><td class="py-0">`
            $.each(result.categories, function (key2, val2) {
                detailCourse += `<span class="badge badge-pill badge-success">${val2.name}</span> `
                
            })
            if (result.status == "Incomplete" || result.status == "Revise") {
                detailCourse += `<button type="button" class="float-right btn btn-sm btn-primary float-right" data-toggle="modal" data-target="#formInputCatalogue"><i class="fa fa-plus"></i></button></td></tr>`
            }
            detailCourse += `<tr><th>Status</th><td>:</td><td><span class="badge rounded-pill bg-warning">${result.status}</span></td>/tr>`
            detailCourse += `<tr><th>Feedback</th><td>:</td><td>${result.feedback}</td>/tr>`
            detailCourse += `</tbody>`
            $("#table-course").html(detailCourse);
            // Picture Course
            var pictureCourse = "";
            pictureCourse += `<img src="/Upload/images/${result.picture}" class="mx-auto d-block" style="width:100%">`
            $("#picture-course").html(pictureCourse);
            // Button Add Course
            var addCourse = "";
            addCourse += `Content Course`
            if (result.status == "Incomplete" || result.status == "Revise") {
                addCourse += `<button type="button" class="btn btn-sm btn-primary float-right" data-toggle="modal" data-target="#formInputContent"><i class="fa fa-plus"></i></button>`
            }
            $("#btnAddContent").html(addCourse);



            // Content Course
            var detailContent = "";
            $.each(result.contents, function (key, val) {
                detailContent += `<div class="card mb-0">`
                detailContent += `<div class="card-header">`
                detailContent += `<a class="btn" data-toggle="collapse" href="#collapse${key}">${val.title}</a>`
                if (result.status == "Incomplete" || result.status == "Revise") {
                    detailContent += `<button type="button" class="btn btn-sm btn-primary ml-1 float-right" onClick="TambahSubContent('${val.id}')"><i class="fa fa-plus"></i></button> `
                    detailContent += `<button type="button" class="btn btn-sm btn-danger float-right" onClick="HapusContent('${val.id}')"><i class="fa fa-trash"></i></button> `
                }
                detailContent += `</div>`
                detailContent += `<div id="collapse${key}" class="collapse" data-parent="#accordion">`
                detailContent += `<div class="card-body">`
                detailContent += `<div id="accordionSub${key}">`
                // SubContent Course
                $.each(val.subContent, function (key2, val2) {
                    detailContent += `<div class="card mb-0">`
                    detailContent += `<div class="card-header">`
                    detailContent += `<a class="btn" data-toggle="collapse" href="#collapseSub${key + key2}">${val2.title}</a>`
                    if (result.status == "Incomplete" || result.status == "Revise") {
                        detailContent += `<button type = "button" class="btn btn-sm btn-danger float-right" onClick = "HapusSubContent('${val2.id}')" > <i class="fa fa-trash"></i></button > `
                    }
                    detailContent += `</div>`
                    detailContent += `<div id="collapseSub${key + key2}" class="collapse" data-parent="#accordionSub${key}">`
                    detailContent += `<div class="card-body">`
                    detailContent += `<div class="row">`
                    detailContent += `<div class="col-md-8">`
                    detailContent += `<div class="border-bottom mb-2">`
                    detailContent += `<h5 class="mb-3"><b>Video</b> </h5>`
                    detailContent += `</div>`
                    detailContent += `<video class="mx-auto d-block" style="width:100%" controls>`
                    detailContent += `<source src="/Upload/videos/${val2.videoName}">`
                    detailContent += `</video>`
                    detailContent += `</div>`
                    detailContent += `<div class="col-md-4">`
                    detailContent += `<div class="border-bottom mb-2">`
                    detailContent += `<h5 class="mb-3">`
                    detailContent += `<b>Source</b>`
                    if (result.status == "Incomplete" || result.status == "Revise") {
                        detailContent += `<button type="button" class="btn btn-sm btn-primary float-right" onClick="TambahResource('${val2.id}')"><i class="fa fa-plus"></i></button>`
                    }
                    detailContent += `</h5>`
                    detailContent += `</div>`
                    detailContent += `<div class="list-group">`
                    // Resource SubContent
                    $.each(val2.resources, function (key3, val3) {
                        detailContent += `<div class="list-group-item list-group-item-action">`
                        detailContent += `<a href="/Upload/resources/${val3.fileName}" target="_blank" >${val3.fileName}</a>`
                        if (result.status == "Incomplete" || result.status == "Revise") {
                            detailContent += `<button type="button" class="btn btn-sm btn-danger float-right" onClick="HapusResource('${val3.id}')" > <i class="fa fa-trash"></i></button></div>`
                        }
                    })
                    detailContent += `</div>`
                    detailContent += `</div>`
                    detailContent += `<div class="col-md-12">`
                    detailContent += `<div class="border-bottom mb-2 mt-4">`
                    detailContent += `<h5 class="mb-3">`
                    detailContent += `<b>Quiz</b>`
                    detailContent += `<button type="button" class="btn btn-sm btn-primary float-right" onClick="TambahQuiz('${val2.id}')"><i class="fa fa-plus"></i></button>`
                    detailContent += `</h5>`
                    detailContent += `</div>`
                    detailContent += `<div class="row">`
                    // Quiz SubContent
                    $.each(val2.quizzes, function (key4, val4) {
                        var num = key4;
                        detailContent += `<div class="col-md-6">`
                        detailContent += `<div class="row">`
                        detailContent += `<div class="col-md-12">`
                        detailContent += `<h5 class="mb-2 mt-4"><b>${num + 1}. ${val4.question}</b></h5>`
                        detailContent += `</div>`
                        $.each(val4.answers, function (key5, val5) {
                            detailContent += `<div class="col-md-6">${val5.contents}`
                            if (val5.isCorrect) {
                                detailContent +=' <i class="fa fa-check text-success"></i>'
                            }
                            detailContent += `</div>`
                        })
                        detailContent += `</div>`
                        detailContent += `</div>`
                    })
                    detailContent += `</div>`
                    detailContent += `</div>`
                    detailContent += `</div>`
                    detailContent += `</div>`
                    detailContent += `</div>`
                    detailContent += `</div>`
                })
                detailContent +=`</div>`
                detailContent +=`</div>`
                detailContent +=`</div>`
                detailContent +=`</div>`
            })
            $("#accordion").html(detailContent);


            if (result.status == "Incomplete" || result.status == "Revise") {
                var statusCourse = "";
                statusCourse += `<button class="btn btn-primary mt-3 mb-5" onclick="CourseSelesai()">Selesai</button>`
                $("#CourseChangeStatus").html(statusCourse);
            } else {
                var statusCourse = "";
                statusCourse += ``
                $("#CourseChangeStatus").html(statusCourse);
            }
            
        }
    })
}
// Dropdown add catalogue
function FormCategory() {
    $.ajax({
        async: false,
        url: "/Categories/getall/",
        success: function (result) {
            var text = "";
            $.each(result, function (key, val) {
                text += `<option value="${val.id}">${val.name}</option>`
            })
            $("#categoryId").html(text);
        }
    })
}
/*============================ Course View End ============================*/


/*============================ Insert Functions ============================*/
$("#submitButtonContent").click(function () {
    var form = $(".needs-validation")
    event.preventDefault();
    if (form[0].checkValidity() === false) {
        event.stopPropagation();
    } else {
        InsertContent();
    }
    form.addClass('was-validated');
})
function InsertContent() {
    var url = window.location.pathname
    var id = url.substring(url.lastIndexOf('/') + 1)
    var obj = new Object();
    obj.Title = $("#titleContent").val();
    obj.CourseId = parseInt(id);
    console.log(obj)
    $.ajax({
        url: "/Contents/Post/",
        type: "POST",
        contentType: 'application/json',
        data: JSON.stringify(obj),
        beforeSend: function () {
            swal.fire({
                title: 'Menunggu',
                html: 'Memproses data',
                didOpen: () => {
                    swal.showLoading()
                }
            })
        }
    }).done((result) => {
        detailCourse();
        $(".needs-validation").removeClass('was-validated');
        $('#formInputContent').find('form').trigger('reset');
        $('#formInputContent').modal('hide');
        console.log(result)
        Swal.fire(
            'Berhasil',
            'Content Telah Ditambahkan',
            'success'
        )
    }).fail((error) => {
        console.log(error)
        Swal.fire(
            'Gagal',
            error.responseText,
            'error'
        )
    })
}

function TambahSubContent(nik) {
    $('#formInputSubContent').modal('show');
    $("#contentId").val(nik);
}
$("#submitButtonSubContent").click(function () {
    var form = $(".needs-validation-subcontent")
    event.preventDefault();
    if (form[0].checkValidity() === false) {
        event.stopPropagation();
    } else {
        InsertSubContent();
    }
    form.addClass('was-validated');
})
function InsertSubContent() {
    var formData = new FormData();
    formData.append('video', $('#videoSubContent').get(0).files[0]);
    formData.append('title', $("#titleSubContent").val());
    formData.append('contentId', parseInt($("#contentId").val()));
    console.log(formData)
    $.ajax({
        url: "/SubContents/Upload/",
        type: "POST",
        processData: false,
        contentType: false,
        cache: false,
        data: formData,
        enctype: 'multipart/form-data',
        beforeSend: function () {
            swal.fire({
                title: 'Menunggu',
                html: 'Memproses data',
                didOpen: () => {
                    swal.showLoading()
                }
            })
        }
    }).done((result) => {
        detailCourse();
        $(".needs-validation").removeClass('was-validated');
        $('#formInputSubContent').find('form').trigger('reset');
        $('#formInputSubContent').modal('hide');
        console.log(result)
        Swal.fire(
            'Berhasil',
            'Sub Content Telah Ditambahkan',
            'success'
        )
    }).fail((error) => {
        console.log(error)
        Swal.fire(
            'Gagal',
            error.responseText,
            'error'
        )
    })
}

function TambahResource(nik) {
    $('#formInputResource').modal('show');
    $("#subContentId").val(nik);
}
$("#submitButtonResource").click(function () {
    var form = $(".needs-validation-resource")
    event.preventDefault();
    if (form[0].checkValidity() === false) {
        event.stopPropagation();
    } else {
        InsertResource();
    }
    form.addClass('was-validated');
})
function InsertResource() {
    var formData = new FormData();
    var fileUpload = $('#fileResource').get(0).files;
    for (var i = 0; i < fileUpload.length; i++) {
        formData.append('file', fileUpload[i]);
    }
    formData.append('subContentId', parseInt($("#subContentId").val()));
    console.log(formData)
    $.ajax({
        url: "/Resources/Upload/",
        type: "POST",
        processData: false,
        contentType: false,
        cache: false,
        data: formData,
        enctype: 'multipart/form-data',
        beforeSend: function () {
            swal.fire({
                title: 'Menunggu',
                html: 'Memproses data',
                didOpen: () => {
                    swal.showLoading()
                }
            })
        }
    }).done((result) => {
        detailCourse();
        $(".needs-validation").removeClass('was-validated');
        $('#formInputResource').find('form').trigger('reset');
        $('#formInputResource').modal('hide');
        console.log(result)
        Swal.fire(
            'Berhasil',
            'Sub Content Telah Ditambahkan',
            'success'
        )
    }).fail((error) => {
        console.log(error)
        Swal.fire(
            'Gagal',
            error.responseText,
            'error'
        )
    })
}

$("#submitButtonCatalogue").click(function () {
    var form = $(".needs-validation-catalogue")
    event.preventDefault();
    if (form[0].checkValidity() === false) {
        event.stopPropagation();
    } else {
        InsertCatalogue();
    }
    form.addClass('was-validated');
})
function InsertCatalogue() {
    var url = window.location.pathname
    var id = url.substring(url.lastIndexOf('/') + 1)
    var obj = new Object();
    obj.CategoryId = $("#categoryId").val();
    obj.CourseId = parseInt(id);
    $.ajax({
        url: "/Catalogues/Post/",
        type: "POST",
        contentType: 'application/json',
        data: JSON.stringify(obj),
        beforeSend: function () {
            swal.fire({
                title: 'Menunggu',
                html: 'Memproses data',
                didOpen: () => {
                    swal.showLoading()
                }
            })
        }
    }).done((result) => {
        detailCourse();
        $(".needs-validation").removeClass('was-validated');
        $('#formInputCatalogue').find('form').trigger('reset');
        $('#formInputCatalogue').modal('hide');
        console.log(result)
        Swal.fire(
            'Berhasil',
            'Content Telah Ditambahkan',
            'success'
        )
    }).fail((error) => {
        console.log(error)
        Swal.fire(
            'Gagal',
            error.responseText,
            'error'
        )
    })
}

function TambahQuiz(nik) {
    $('#formInputQuiz').modal('show');
    $("#subContentIdQuiz").val(nik);
}
$("#submitButtonQuiz").click(function () {
    var form = $(".needs-validation-quiz")
    event.preventDefault();
    if (form[0].checkValidity() === false) {
        event.stopPropagation();
    } else {
        InsertQuiz();
    }
    form.addClass('was-validated');
})
function InsertQuiz() {
    var obj = new Object();
    obj.Question = $("#question").val();
    obj.SubContentId = parseInt($("#subContentIdQuiz").val())
    obj.Answer1 = $("#answer1").val();
    obj.IsCorrect1 = parseInt($("[name=isCorrect1]:checked").val());
    obj.Answer2 = $("#answer2").val();
    obj.IsCorrect2 = parseInt($("[name=isCorrect2]:checked").val());
    obj.Answer3 = $("#answer3").val();
    obj.IsCorrect3 = parseInt($("[name=isCorrect3]:checked").val());
    obj.Answer4 = $("#answer4").val();
    obj.IsCorrect4 = parseInt($("[name=isCorrect4]:checked").val());
    console.log(obj)
    $.ajax({
        url: "https://localhost:44385/api/quizzes/addquiz",
        type: "POST",
        contentType: 'application/json',
        data: JSON.stringify(obj),
        beforeSend: function () {
            swal.fire({
                title: 'Menunggu',
                html: 'Memproses data',
                didOpen: () => {
                    swal.showLoading()
                }
            })
        }
    }).done((result) => {
        console.log(result)
        detailCourse();
        $(".needs-validation").removeClass('was-validated');
        $('#formInputQuiz').find('form').trigger('reset');
        $('#formInputQuiz').modal('hide');
        console.log(result)
        Swal.fire(
            'Berhasil',
            'Quiz Telah Ditambahkan',
            'success'
        )
    }).fail((error) => {
        console.log(error)
        Swal.fire(
            'Gagal',
            error.responseText,
            'error'
        )
    })
}

/*============================ Insert Functions End ============================*/


/*============================ Delete Functions ============================*/
function HapusContent(id) {
    event.preventDefault();
    Swal.fire({
        title: 'Hapus Data',
        text: "Anda yakin ingin menghapus data ini ?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ya',
        cancelButtonText: 'Batal'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/contents/delete/" + id,
                type: "DELETE",
                beforeSend: function () {
                    swal.fire({
                        title: 'Menunggu',
                        html: 'Memproses data',
                        didOpen: () => {
                            swal.showLoading()
                        }
                    })
                }
            }).done((result) => {
                detailCourse();
                Swal.fire(
                    'Berhasil',
                    result,
                    'success'
                )
            }).fail((error) => {
                Swal.fire(
                    'Gagal',
                    error.responseText,
                    'error'
                )
            })
        }
    })
}
function HapusSubContent(id) {
    event.preventDefault();
    Swal.fire({
        title: 'Hapus Data',
        text: "Anda yakin ingin menghapus data ini ?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ya',
        cancelButtonText: 'Batal'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/subcontents/delete/" + id,
                type: "DELETE",
                beforeSend: function () {
                    swal.fire({
                        title: 'Menunggu',
                        html: 'Memproses data',
                        didOpen: () => {
                            swal.showLoading()
                        }
                    })
                }
            }).done((result) => {
                detailCourse();
                Swal.fire(
                    'Berhasil',
                    result,
                    'success'
                )
            }).fail((error) => {
                Swal.fire(
                    'Gagal',
                    error.responseText,
                    'error'
                )
            })
        }
    })
}
function HapusResource(id) {
    event.preventDefault();
    Swal.fire({
        title: 'Hapus Data',
        text: "Anda yakin ingin menghapus data ini ?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ya',
        cancelButtonText: 'Batal'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/resources/delete/" + id,
                type: "DELETE",
                beforeSend: function () {
                    swal.fire({
                        title: 'Menunggu',
                        html: 'Memproses data',
                        didOpen: () => {
                            swal.showLoading()
                        }
                    })
                }
            }).done((result) => {
                detailCourse();
                Swal.fire(
                    'Berhasil',
                    result,
                    'success'
                )
            }).fail((error) => {
                Swal.fire(
                    'Gagal',
                    error.responseText,
                    'error'
                )
            })
        }
    })
}
/*============================ Delete Functions End ============================*/


/*============================ Change Status Course ============================*/
function CourseSelesai() {
    var url = window.location.pathname
    var id = url.substring(url.lastIndexOf('/') + 1)
    var obj = new Object();
    obj.status = 1;
    obj.Id = parseInt(id);
    $.ajax({
        url: "/courses/updateStatus",
        type: "PUT",
        contentType: 'application/json',
        data: JSON.stringify(obj),
        beforeSend: function () {
            swal.fire({
                title: 'Menunggu',
                html: 'Memproses data',
                didOpen: () => {
                    swal.showLoading()
                }
            })
        }
    }).done((result) => {
        detailCourse();
        console.log(result)
        Swal.fire(
            'Berhasil',
            'Course Anda sedang Ditinjau Oleh Admin',
            'success'
        )
    }).fail((error) => {
        console.log(error)
        Swal.fire(
            'Gagal',
            error.responseText,
            'error'
        )
    })
}
/*============================ Change Status Course End ============================*/
