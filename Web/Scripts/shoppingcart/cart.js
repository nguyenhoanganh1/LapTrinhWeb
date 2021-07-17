
$(function () {
    InfoCart();

    function InfoCart() {
        $.ajax({
            url: `/shoppingcart/InfoCart`,
            success: function (resp) {
                console.log(resp);
                document.getElementById("soluong").innerHTML = resp.count;
                document.getElementById("tongtien").innerHTML = resp.amount + " VNĐ";
            }

        });
    }


    $('.add-to-cart').click(function () {
        var id = $(this).parents("[data-item]").attr("data-item");
        console.log(id);
        $.ajax({
            url: `/shoppingcart/additem?id=${id}`,
            method: 'POST',
            success: function () {
                alert('Thêm vào giỏ hàng thành công');
            }

        });
    })
})
