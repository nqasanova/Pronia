$(document).on("click", ".show-plant-modal", function (e) {
    console.log("Hello 1");

    e.preventDefault();

    var url = e.target.href;
    console.log(url)

    fetch(url)
        .then(response => response.text())
        .then(data => {
            $('.plant-details-modal').html(data);
            console.log(data)
        })

    $("#quickModal").modal("show");
})


let btns = document.querySelectorAll(".add-product-to-basket-btn")

btns.forEach(x => x.addEventListener("click", function (e) {
    console.log("Hello 2");

    e.preventDefault()
    console.log(e.target.href)
    fetch(e.target.href)
        .then(response => response.text())
        .then(data => {
            console.log(data)
            $('.cart-block').html(data);
        })
}))

$(document).on("click", ".add-product-to-basket-modal", function (e) {
    console.log("Hello 1");

    e.preventDefault();

    fetch(e.target.href)
        .then(response => response.text())
        .then(data => {
            $('.cart-block').html(data);
        })
})

$(document).on("click", ".add-details-basket", function (e) {
    console.log("Hello 1");

    e.preventDefault();

    fetch(e.target.href)
        .then(response => response.text())
        .then(data => {
            $('.cart-block').html(data);
        })
})






$(document).on("click", ".remove-product-to-basket-btn", function (e) {
    console.log("Hello 1");

    e.preventDefault();

    fetch(e.target.parentElement.href)
        .then(response => response.text())
        .then(data => {
            $('.cart-block').html(data);
        })
})


$(document).on("click", ".plus-btn", function (e) {
    console.log("Hello 1");

    e.preventDefault();

    fetch(e.target.href)
        .then(response => response.text())
        .then(data => {
            $('.cartPageJs').html(data);

            fetch(e.target.nextElementSibling.href)
                .then(response => response.text())
                .then(data => {
                    $('.cart-block').html(data);
                })
        })
})

$(document).on("click", ".minus-btn", function (e) {
    console.log("Hello 1");

    e.preventDefault();

    fetch(e.target.href)
        .then(response => response.text())
        .then(data => {
            $('.cartPageJs').html(data);

            fetch(e.target.nextElementSibling.href)
                .then(response => response.text())
                .then(data => {
                    $('.cart-block').html(data);
                })
        })
})




$(document).on("change", '.searchProductByPrice', function (e) {
    console.log("Hello 1");


    e.preventDefault();
    let minPrice = e.target.previousElementSibling.children[0].children[3].innerText.slice(1);
    let MinPrice = parseInt(minPrice);
    let maxPrice = e.target.previousElementSibling.children[0].children[4].innerText.slice(1);
    let MaxPrice = parseInt(maxPrice);
    let aHref = document.querySelector(".productBySearchQuery").href;
    console.log(MinPrice)
    console.log(MaxPrice)
    console.log(aHref)
    $.ajax(
        {
            url: aHref,

            data: {
                MinPrice: MinPrice,
                MaxPrice: MaxPrice

            },

            success: function (response) {
                FilterSlider.html(response);




            },
            error: function (err) {
                $(".product-details-modal").html(err.responseText);

            }

        });

})