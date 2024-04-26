document.querySelectorAll('.rating input[type="radio"]').forEach(function (star) {
    star.addEventListener('click', function () {
        var ratingValue = this.value;

        fetch('@Url.Action("Rate", "Products")', {
            method: 'POST',
            body: JSON.stringify({ model: { ProductId: @Model.ProductId, NumberOfStars: ratingValue } }),
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(response => {
                if (response.ok) {
                    alert('Rating submitted successfully!');
                } else {
                    alert('Failed to submit rating. Please try again later.');
                }
            })
            .catch(error => {
                console.error('Error:', error);
                alert('An error occurred. Please try again later.');
            });
    });
});
document.querySelectorAll('.rating input[type="radio"]').forEach(function (star) {
    star.addEventListener('click', function () {
        var ratingValue = this.value;
        var productId = this.id.substring(1);

        document.getElementById('ratingValue-' + productId).value = ratingValue;

        document.getElementById('rateForm-' + productId).submit();
    });
});
