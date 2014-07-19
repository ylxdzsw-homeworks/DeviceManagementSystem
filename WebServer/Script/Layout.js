
console.log("console前的少年,发现BUG了吗？\n发现BUG记得告诉我哦");
window.onload = function () {
    var lastscrolllocation = 0;
    var headroom = document.getElementById("nav-bar");
    window.onscroll = function () {
        var nowscrolllocation = document.documentElement.scrollTop || document.body.scrollTop;
        console.log(nowscrolllocation);
        if (lastscrolllocation < 102) {
            headroom.className = "headroom--toped";
        }
        else if (nowscrolllocation > lastscrolllocation && nowscrolllocation > 100) {
            headroom.className = "headroom--unpinned";
        } else {
            headroom.className = "headroom--pinned";
        }
        lastscrolllocation = nowscrolllocation;
    }
}