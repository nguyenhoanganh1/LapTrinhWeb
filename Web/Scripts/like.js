$(function () {
    /*Hiện thị mặt hàng yêu thích*/
    GetAllLike();

    function GetAllLike() {
        $.ajax({
            url: '/GetAllLike',
            method: 'GET',
            success: function (resp) {
                resp.data.forEach(x => {
                    var tr = `<div class="col-xs-4 text-center">
                        <a href="/Products/Details/${x.id}">
                        <img src="/Content/images/items/${x.image}">
                        </a> 
                        </div>`;

                    $("#favos").append(tr);
                    console.log(resp.data);
                })

            }
        })
    }

    $('.add-to-favo').click(function () {
        var id = $(this).parents("[data-item]").attr("data-item");

        $.ajax({
            url: `/Products/LikeProduct?id=${id}`,
            method: 'POST',
            success: function () {
                alert('Yêu thích thành công');
            }

        });
    });
})