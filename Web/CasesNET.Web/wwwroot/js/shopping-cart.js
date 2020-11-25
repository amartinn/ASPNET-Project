
$(document).ready(function () {

    $('.shopping-cart-form').submit(async function (e) {
        e.preventDefault();
        const url = $(this).attr('action');
        const antiForgerytoken = $(this.elements['__RequestVerificationToken']).val();
        await $.ajax({
            url,
            method:"POST",
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json",
                "RequestVerificationToken": antiForgerytoken
            }
        })
        const count = await GetCount();
            updateBagQuantity(count);
    })

    
})
async function GetCount() {
    const url = 'https://localhost:44319/api/cart/count';
    const data = await $.get({
        url: url,
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json",
        }
    });
    return data;
}