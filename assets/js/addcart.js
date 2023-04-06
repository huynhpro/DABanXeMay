$(document).ready(function () {
    $(".ajax-add-to-cart").click(function () {
        $.ajax({
            url: "/GioHang/ThemGioHang",
            data: {
                idSanPham: $(this).data("id"),
                soluong: 1,
                type: "ajax"
            },
            success: function (data) {
                console.log(data);
                Swal.fire({
                    position: 'top-end',
                    icon: 'error',
                    title: 'thêm thất bại',
                    showConfirmButton: false,
                    timer: 1500
                });
                
                $("#cart_count").html(data.soluong)
            },
            error: function () {
                Swal.fire({
                    icon: 'success',
                    title: 'Thêm thành công',
                    showConfirmButton: false,
                    timer: 1500
                });
            }
        });
    });
});