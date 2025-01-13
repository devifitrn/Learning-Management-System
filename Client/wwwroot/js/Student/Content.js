Course();
/*SubCourse();*/
/*Transaction();*/

Reviews();

function Course() {
    var url = window.location.pathname
    var id = url.substring(url.lastIndexOf('/') + 1)
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
                        <h3>${result.title}</h3><br>`
            var avg = AvgRating(id);
                text +=`
                        <i class="fa fa-star text-warning" style="font-size:1.5rem;"></i> <b style="font-size:1.5rem;">${avg.rating} </b> <small>(${avg.review})</small><br><br>`
                text +=`        <p class="card-text">${result.description}</p>
                        <h4 class="card-text">${"Rp. " + result.price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".")}</h4>`
            if (userId != null) {
                var rev = CheckEnrollment(result.id);
                if (rev == null) {
                    text += `<button class="btn btn-dark" onClick="BuyCourse()"> buy course </button>`
                } else {
                    text += `<td><a class="btn py-1 btn-primary" href="/Student/CourseView/${rev.id}">Mulai Course</a>`
                }
            } else {
                text += `<button class="btn btn-dark" onClick="BuyCourse()"> buy course </button>`
            }
            text +=  `</div>`;
            $("#subContentCourse").html(text);

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
            $("#accordion").html(detailContent);
        }
    })
}
/*function BuyCourse() {
    if (userId != null) {
        Swal.fire({
            icon: 'warning',
            title: 'Konfirmasi Beli',
            text: 'Apakah Anda Yakin Membeli Course Ini?',
            showCancelButton: true,
            confirmButtonText: 'Ya',
            cancelButtonText: 'Batal'
        }).then(function (result) {
            if (result.isConfirmed) {
                var date = (new Date(new Date().getTime() - (new Date().getTimezoneOffset() * 60 * 1000))).toISOString();
                var courseId = window.location.pathname.substring(window.location.pathname.lastIndexOf('/') + 1)
                var obj = new Object();
                obj.UserId = userId;
                obj.CourseId = courseId;
                obj.StartDate = date;
                console.log(obj)
                $.ajax({
                    url: "/Enrollment/Post/",
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
                    Swal.fire(
                        'Berhasil',
                        'Course Berhasil Dibeli',
                        'success'
                    ).then(function () {
                        window.location = "/Student/";
                    });
                }).fail((error) => {
                    console.log(error)
                    Swal.fire(
                        'Gagal',
                        error.responseText,
                        'error'
                    )
                })

            }
        });
    } else {
        Swal.fire({
            icon: 'warning',
            title: 'Harap login Terlebih Dahulu',
            showCancelButton: true,
            confirmButtonText: 'Ke Halaman Login',
            cancelButtonText: 'Batal'
        }).then(function (result) {
            if (result.isConfirmed) {
                window.location = "/Student/";
            }
        });
    } 
}*/


/*function Transaction(id) {
    event.preventDefault();
    var obj = new Object();
    obj.userId = $("#userId").val("2022002");
    obj.courseId = $("#courseId").val(id);
    obj.startDate = $("#startDate").val();
    console.log(obj)

    $.ajax({
        url: "/enrollment/post" + id, 
        type: 'post',
        data: obj,
        dataType: "JSON"

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
    
}*/
/*ngrok http https://localhost:44317 --host-header="localhost:44317"*/ 


function BuyCourse() {
    var url = window.location.pathname
    var id = url.substring(url.lastIndexOf('/') + 1)
    $.ajax({
        async:false,
        url: `/courses/get/${id}`,
        success: function (results) {
            crs = results;
        }
    });
    console.log(crs)
    if (userId != null) {
        Swal.fire({
            icon: 'warning',
            title: 'Konfirmasi Beli',
            text: 'Apakah Anda Yakin Membeli Course Ini?',
            showCancelButton: true,
            confirmButtonText: 'Ya',
            cancelButtonText: 'Batal'
        }).then(function (result) {
            if (result.isConfirmed) {
                if (crs.price != 0) {
                    var date = (new Date(new Date().getTime() - (new Date().getTimezoneOffset() * 60 * 1000))).toISOString();
                    var courseId = window.location.pathname.substring(window.location.pathname.lastIndexOf('/') + 1)
                    $.ajax({
                        url: "/enrollment/mid",
                        method: 'POST',
                        contentType: 'application/json',
                        data: JSON.stringify({
                            'enrollment': {
                                'userId': userId,
                                'courseId': courseId,
                                'startDate': date
                            },
                            'transaction_details': {
                                'order_id': 'demo-docs-main-' + Math.round((new Date()).getTime() / 1),
                                'gross_amount': crs.price
                            },
                            'credit_card': {
                                'secure': true
                            },
                            "callbacks": {
                                "finish": "https://c5c0-140-213-7-144.ap.ngrok.io/student"
                            }
                        })
                    }).done((result) => {
                        console.log(result)
                        snap.pay(result.token, {
                            onClose: function () {
                                /* You may add your own implementation here */
                                alert('you closed the popup without finishing the payment');
                            }
                        })
                    }).fail((error) => {
                        console.log(error)
                        Swal.fire(
                            'failed',
                            'error'
                        )
                    });
                } else {
                    var date = (new Date(new Date().getTime() - (new Date().getTimezoneOffset() * 60 * 1000))).toISOString();
                    var courseId = window.location.pathname.substring(window.location.pathname.lastIndexOf('/') + 1)
                    var obj = new Object();
                    obj.UserId = userId;
                    obj.CourseId = courseId;
                    obj.StartDate = date;
                    obj.status = 2;
                    console.log(obj)
                    $.ajax({
                        url: "/Enrollment/Post/",
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
                        Swal.fire(
                            'Berhasil',
                            'Course Berhasil Dibeli',
                            'success'
                        ).then(function () {
                            window.location = "/Student";
                        });
                    }).fail((error) => {
                        console.log(error)
                        Swal.fire(
                            'Gagal',
                            error.responseText,
                            'error'
                        )
                    })
                }

            }
        });
    } else {
        Swal.fire({
            icon: 'warning',
            title: 'Harap login Terlebih Dahulu',
            showCancelButton: true,
            confirmButtonText: 'Ke Halaman Login',
            cancelButtonText: 'Batal'
        }).then(function (result) {
            if (result.isConfirmed) {
                window.location = "/Student/";
            }
        });
    }
}

function Reviews() {
    var url = window.location.pathname;
    var id = url.substring(url.lastIndexOf('/') + 1);
    $.ajax({
        url: "/reviews/GetCourseReviews/" + id,
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
    console.log(reviews);
    let comments = "";
    for (var i = 0; i < reviews.length; i++) {
        comments +=
            `<h6>${reviews[i].user.firstName + ' ' + reviews[i].user.lastName } ★${reviews[i].rating}</h5>
             <p>${reviews[i].contents}`
    }
    return comments;
}
function CheckEnrollment(id) {
    var check;
    $.ajax({
        async: false,
        url: `/enrollment/CheckEnrollment/${userId}/${id}`,
        success: function (result) {
            check = result;
        }
    })
    return check
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
