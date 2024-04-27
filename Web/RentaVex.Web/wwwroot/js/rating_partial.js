function updateRating(productId, rating) {
    var t = $("input[name='__RequestVerificationToken']").val();
    document.getElementById("ratingValue-" + productId).value = rating;

    $(document).ready(function () {
        function showSuccessToast() {
            $('#successToastContainer').css('opacity', '1');
            $('#successToastContainer').show();

            setTimeout(function () {
                $('#successToastContainer').css('opacity', '0');
                setTimeout(function () {
                    $('#successToastContainer').hide();
                }, 500);
            }, 3000);
        }

        $.ajax({
            headers:
            {
                "RequestVerificationToken": t
            },
            type: 'POST',
            url: '/Products/Rate',
            data: { "productId": productId, "ratingValue": rating },
            dataType: 'json',
            success: function (response) {

                console.log('Rating submitted successfully');
                if (response.success) {
                    $('.product-rating' + productId).text(response.rating.toFixed(2) + " / 5");
                    showSuccessToast();

                } else {
                    console.error('Rating submission failed:', response.error);
                }
            },
            error: function (xhr, status, error) {
                console.error('Error submitting rating:', error);
            }
        });
    });


}

function toggleLikeImage(productId, button) {
    var darkLikeImage = document.getElementById('darkLikeImage_' + productId);
    var redLikeImage = document.getElementById('redLikeImage_' + productId);

    if (darkLikeImage.style.display === 'none') {
        darkLikeImage.style.display = 'inline-block';
        redLikeImage.style.display = 'none';
    } else {
        darkLikeImage.style.display = 'none';
        redLikeImage.style.display = 'inline-block';

        button.removeAttribute('onclick');
    }
}
