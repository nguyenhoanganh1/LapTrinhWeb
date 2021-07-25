
$(function () {
    InfoCart();

    $('.share').click(function () {
        var id = $(this).parents("[data-item]").attr("data-item");
        var send = { From: $('#sender').val(), To: $('#receiver').val(), Subject: $('#subject').val(), Content: $('#content').val() };
        console.log(send);
        console.log(id);
        $.ajax({
            url: `/Products/SendMail/${id}`,
            method: 'POST',
            data: send,
            success: function (resp) {
                console.log(resp.data);
                alert("Chia sẽ thành công");
                $('#myModal').modal('hide');
            },
        })
    })
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
