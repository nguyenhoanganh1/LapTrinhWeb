
    $(function () {
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
  