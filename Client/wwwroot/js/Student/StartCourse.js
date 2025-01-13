
/*SubCourse();*/
/*Transaction();*/
$(document).ready(function () {
    //GetCourseId();
    StartCourse();
    console.log(userId);
    ButtonFinish();
    Reviews();
});
function CourseId() {
    var url = window.location.pathname
    var id = url.substring(url.lastIndexOf('/') + 1)
    $.ajax({
        async:false,
        url: "/enrollment/get/" + id,
        success: function (result) {
            courseID = result.courseId
        }
    })
    return courseID
}

function StartCourse() {
    var id = CourseId();
    $.ajax({
        url: "/Courses/getmasterdata/" + id,
        success: function (result) {
            console.log(result.result);
            var result = result.result

            var resources = "";
            resources += `<img width=98% src="/Upload/images/${result.picture}"/>`

            $("#resources").html(resources);

            var titleCourse = "";
            titleCourse += `<h4>${result.title}</h4>`
            $("#titleCourse").html(titleCourse);

            var detailContent = "";
            $.each(result.contents, function (key1, val1) {
                detailContent += `<div class="panel panel-default">
                                    <div class="panel-heading card-header">
                                        <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapse1${key1}">
                                            <h5 id="titleContent">
                                              <i class="fa fa-object-group fa-lg"></i>${val1.title}
                                            </h5>
                                        </a>
                                    </div>`

                $.each(val1.subContent, function (key2, val2) {
                    detailContent += `<div id="collapse1${key1}" class="panel-collapse collapse">`
                    detailContent += `<div class="panel-body">`
                    detailContent += `<div class="panel-group" id="accordion2${key2}">`
                    detailContent += `<div class="panel panel-default">`
                    detailContent += `<div class="row mt-2 mb-2">
                                        <div class="panel-heading col-10">
                                            <a class="collapsed" data-toggle="collapse" data-parent="#accordion2${key2}" href="#collapse11${key1 + key2}">
                                               <div class="panel body ml-4" onclick="Video(${val2.id})"><h5>${val2.title}</h5></div>
                                            </a>
                                        </div>`
                    detailContent += `<div class="dropdown col">
                                        <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenu2" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                           Resources
                                        </button>`
                    detailContent += `<div class="dropdown-menu" aria-labelledby="dropdownMenu2">`
                    $.each(val2.resources, function (key3, val3) {
                        detailContent += `<button class="dropdown-item" type="button" >
                                            <a href="/Upload/resources/${val3.fileName}" class="button"><i class="fa fa-download"></i>Modul ${val3.id++}</a>
                                          </button>`
                    })

                    detailContent +=`</div>
                                     </div>
                                     </div>
                                     </div>
                                     </div>
                                     </div>
                                     </div>`
                        console.log(val2.videoName)
                    
                    detailContent += `</div>`
                })
                $("#accordion").html(detailContent);
            })
        }
    })
}

function Course() {
    var id = CourseId();
    $.ajax({
        url: "/Courses/getmasterdata/" + id,
        success: function (result) {
            console.log(result.result);
            var result = result.result
            var text = "";

            text += `<div class="col">
                        <img class="card-img-top" src="/upload/images/${result.picture}" alt="Card image cap">
                    </div>
                    <div class="col">
                        <h3>${result.title}</h5><br>
                        <p class="card-text">${result.description}</p>
                        <h4 class="card-text">Rp ${result.price}</h4>
                    </div>`;
            $("#subContentCours").html(text);

            var detailContent = "";
            $.each(result.contents, function (key1, val) {

                detailContent +=
                    `<div class="card">
                        <div class="card-header py-1">
                            <a class="collapsed card-link" data-toggle="collapse" href="#collapse${key1}">
                                ${val.title}
                            </a>
                        </div>
                        <div id="collapse${key1}" class="collapse" data-parent="#accordion">
                            <div class="card-body">
                                <table width="100%">`
                $.each(val.subContent, function (key2, val2) {
                    detailContent += `<tr><td>${val2.title}</td><td class="text-right">${val2.duration}</td></tr>`
                })
                detailContent +=
                    `</table>
                            </div>
                        </div>
                    </div>`


                $.each(val.subContent, function (key2, val2) {

                })
            })
            $("#accordio").html(detailContent);
        }
    })
}

function Video(idSubContent) {
    var id = CourseId();
    $.ajax({
        url: "/Courses/getmasterdata/" + id,
        success: function (result) {
            console.log(result.result);
            var result = result.result

            var video = "";
            $.each(result.contents, function (key1, val1) {

                $.each(val1.subContent, function (key2, val2) {
                    if (idSubContent == val2.id) {
                        video += `<video class="mx-auto d-block" style="width:100%" controls>
                                        <source src="/upload/videos/${val2.videoName}">
                                      </video>`
                    }

                })
            })

            $("#resources").html(video);
        }
    }).then(function (result) {
        window.scroll({
            top: 0,
            left: 0,
            behavior: 'smooth'
        });
    });
}


function Quiz(idQuiz) {
    var id = CourseId();
    $.ajax({
        url: "/Courses/getmasterdata/" + id,
        success: function (result) {
            console.log(result.result);
            var result = result.result

            var video = "";
            $.each(result.contents, function (key1, val1) {

                $.each(val1.subContent, function (key2, val2) {

                    $.each(val2.resources, function (key3, val3) {
                        if (idQuiz == val3.id) {
                            video += `<a>${val3.Quiz}</a>`
                        }
                        
                    })
                })
            })
            $("#resources").html(video);
        }
    })
}

function EnrollmentFinish() {
    Swal.fire({
        icon: 'warning',
        title: 'Konfirmasi',
        text: 'Apakah Anda Yakin Menyelesaikan Course Ini?',
        showCancelButton: true,
        confirmButtonText: 'Ya',
        cancelButtonText: 'Batal'
    }).then(function (result) {
        var id = window.location.pathname.substring(window.location.pathname.lastIndexOf('/') + 1)
        $.ajax({
            async: false,
            url: "/enrollment/get/" + id,
            success: function (result) {
                var enr = {
                    id: result.id,
                    userId: result.userId,
                    courseId: result.courseId,
                    isComplete: true,
                    status: result.status,
                    startDate: result.startDate,
                    endDate: (new Date(new Date().getTime() - (new Date().getTimezoneOffset() * 60 * 1000))).toISOString()
                };
                $.ajax({
                    url: "/enrollment/put",
                    type: "PUT",
                    dataType: 'JSON',
                    data: enr,
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
                    Swal.fire(
                        'Berhasil',
                        'Anda Sudah Menyelesaikan Course Ini',
                        'success'
                    ).then(function () {
                        window.location = "/student";
                        
                    })
                }).fail((error) => {
                    console.log(error)
                    Swal.fire(
                        'Gagal',
                        error.responseText,
                        'error'
                    )
                })
            }
        })
    })
}

function ButtonFinish() {
    var url = window.location.pathname
    var id = url.substring(url.lastIndexOf('/') + 1)
    $.ajax({
        async: false,
        url: "/enrollment/get/" + id,
        success: function (result) {
            if (!result.isComplete) {
                btnFinish = "";
                btnFinish += `<button class="btn btn-success" onClick = "EnrollmentFinish()"> Enrollment Selesai </button>`
                $("#btnFinish").html(btnFinish);
            }
                
        }
    })
}

function ReviewTab() {

}

function Reviews() {
    let courseId = CourseId().toString();
    $.ajax({
        url: "/reviews/GetCourseReviews/" + courseId,
        success: function (reviews) {
            console.log(reviews);
            var ratingAVG = CourseAverageRating(reviews);
            console.log(ratingAVG);
            var text = "";
            text +=
                `<h1>${ratingAVG}</h1>
                 <p>★<br>Course Rating</p>`;
            $('#rating').html(text);
            ratingsChart(reviews);
            text = CourseComments(reviews);
            $('#comments').html(text);
        }
    });

}

function CourseAverageRating(reviews) {
    let sum = 0;
    for (var i = 0; i < reviews.length; i++) {
        sum += reviews[i].rating;
    }
    var average = sum / reviews.length;
    return average.toFixed(2);
}

function ratingsChart(reviews) {
    let ratingsData = [{ x: '1 ★', y: 0 }, { x: '2 ★', y: 0 }, { x: '3 ★', y: 0 }, { x: '4 ★', y: 0 }, { x: '5 ★', y: 0 }];
    $.each(reviews, function (key, review) {
        $.each(ratingsData, function (key, rating) {
            if (review.rating == (key + 1)) {
                ++ratingsData[key].y;
            }
        })
    });
    console.log(ratingsData);
    var optionRating = {
        grid: {
            show: false,
            xaxis: {
                lines: {
                    show: false
                }
            },
            yaxis: {
                lines: {
                    show: false
                }
            }
        },
        chart: {
            type: "bar",
            height: 300,
            toolbar: {
                show: false
            }
        },
        series: [{
            name: "count",
            data: ratingsData
        }],
        plotOptions: {
            bar: {
                borderRadius: 4,
                horizontal: true
            }
        },
        xaxis: {
            type: 'category',
            labels: {
                show: false
            }
        }
    }
    var chart = new ApexCharts(document.querySelector("#ratingsChart"), optionRating)
    chart.render();
}

function CourseComments(reviews) {
    let comments = "";
    for (var i = 0; i < reviews.length; i++) {
        comments +=
            `<h6>${reviews[i].user.firstName + ' ' + reviews[i].user.lastName} ★${reviews[i].rating}</h5>
             <p>${reviews[i].contents}`
    }
    return comments;
}
