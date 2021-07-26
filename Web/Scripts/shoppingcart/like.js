$(function () {
    /*Hiện thị mặt hàng yêu thích*/
    GetAllLike();

    function GetAllLike() {
        $.ajax({
            url: '/GetAllLike',
            method: 'GET',
            success: function (resp) {            
                resp.data.forEach(x => {
                   /* var data = `<div class="col-xs-4 text-center">
                                    <a href="/Products/Details/${resp.data.Id}">
                                        <img src="~/Content/images/items/${resp.data.Image}">
                                    </a>
                                </div>`;
                        */
                    $("#favos").append(x);
                    console.log(x);
                })

            }
        })
    }

    $('.add-to-favo').click(function () {
        var id = $(this).parents("[data-item]").attr("data-item");
        
        $.ajax({
            url: `/Products/like?id=${id}`,
            method: 'POST',
            success: function () {
                alert('Yêu thích thành công');
            }

        });
    });
})