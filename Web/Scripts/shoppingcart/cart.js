﻿
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
       
        var count = 0;
        $.ajax({
            url: `/shoppingcart/InfoCart`,
            success: function (resp) {
                count = resp.count;
                amount = resp.amount;
                console.log(resp);
                document.getElementById("soluong").innerHTML = count;
                document.getElementById("tongtien").innerHTML = amount + " VNĐ";
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
