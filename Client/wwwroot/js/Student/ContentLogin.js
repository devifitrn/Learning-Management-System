Course();
/*SubCourse();*/
/*Transaction();*/


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
                        <img class="card-img-top" src="/assets/img/${result.picture}" alt="Card image cap">
                    </div>
                    <div class="col">
                        <h5 class="card-title">${result.title}</h5>
                        <p class="card-text">${result.description}</p>
                        <h4 class="card-text">Rp ${result.price}</h4>
                        <button class="btn btn-dark" data-toggle="modal" data-target="#buyCourse" >
                        buy course
                    </button>

                    </div>`
                ;
            $("#subContentCourse").html(text);

            var detailContent = "";
           
            $.each(result.Content, function (key1, val) {
                detailContent +=
                    `<div class="card">
                        <div class="card-header" id="headingOne">
                            <h5 class="mb-0" id="btnSubTitle">
                                <button class="btn btn-link" data-toggle="collapse" data-target="#collapseOne" aria-expanded="false" aria-controls="collapseOne">
                                    ${val.title}
                                </button>
                            </h5>
                        </div>
                        `
                
                    $.each(val.SubContent, function (key2, val2) {
                        detailContent +=
                            `<div id="collapseOne" class="collapse" aria-labelledby="headingOne" data-parent="#accordion">
                                <div class="card-body text-md-center">
                                    <div class="row">
                                        <video class="dt-body-center" width="420" height="280" controls>
                                            <source src="https://www.youtube.com/ ${val2.VideoName}" type="video/mp4">
                                        </video>
                                    </div>
                                    <div class="row ml-lg-auto">
                                        Introduction
                                    </div>
                                </div>
                            </div> `
                    })
                detailContent += `</div>`
            })
            console.log(detailContent);
            $("#accordion").html(detailContent);
        }
    })
}



function Transaction() {
    $.ajax({
        url: "/Courses/getmasterdata/",
        success: function (result) {
            console.log(result);
            

        }
    })
}