










class DataUrl {
 ShortenUrl(src) {
    const byteCharacters = atob(src);
    const byteNumbers = new Array(byteCharacters.length);
    for (let i = 0; i < byteCharacters.length; i++) {
        byteNumbers[i] = byteCharacters.charCodeAt(i);
    }
    const byteArray = new Uint8Array(byteNumbers);
    const blob = new Blob([byteArray], { type: 'image/gif' });
    var imageUrl = URL.createObjectURL(blob);
     return imageUrl;
}}
module.exports = new DataUrl;