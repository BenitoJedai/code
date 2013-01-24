
var k = !0, q = !1;
function r(a, b, c) {
    return { x: a || 0, y: b || 0, a: c || 0 }
}
function u(a, b) {
    return { x: a.x + b.x, y: a.y + b.y, a: a.a + b.a }
}
function w(a, b) {
    return { x: a.x - b.x, y: a.y - b.y, a: a.a - b.a }
}
function x(a, b) {
    return { x: a.x / b, y: a.y / b, a: a.a / b }
}
function y(a) {
    return Math.sqrt(a.x * a.x + a.y * a.y + a.a * a.a)
}
function A(a, b) {
    var c = 0.0174532925 * (b.x / 2),
        e = 0.0174532925 * (b.y / 2),
        g = 0.0174532925 * (b.a / 2),
        f = Math.cos(c),
        j = Math.cos(e),
        p = Math.cos(g),
        c = Math.sin(c),
        e = Math.sin(e),
        z = Math.sin(g),
        g = f * j * p + -c * e * z,
        s = c * j * p - -f * e * z,
        t = f * e * p + c * j * -z,
        f = f * j * z - -c * e * p;
    return { x: a.x * (1 - 2 * (t * t + f * f)) + a.y * 2 * (s * t - f * g) + a.a * 2 * (s * f + t * g), y: a.x * 2 * (s * t + f * g) + a.y * (1 - 2 * (s * s + f * f)) + a.a * 2 * (t * f - s * g), a: a.x * 2 * (s * f - t * g) + a.y * 2 * (f * t + s * g) + a.a * (1 - 2 * (s * s + t * t)) }
}
function B(a, b) {
    var c = [];
    if (a instanceof F)
        for (var e = 0; e < a.e.length; e++)
            c = c.concat(B(a.e[e], b));
    else
        a instanceof b && c.push(a);
    return c
}
function aa(a, b) {
    var c = B(a, G);
    c.sort(function (a, c) {
        var f = b.l(), j = ba(a), p = ba(c), j = Math.min(y(w(f, j[0])), y(w(f, j[1])), y(w(f, j[2])), y(w(f, j[3]))), f = Math.min(y(w(f, p[0])), y(w(f, p[1])), y(w(f, p[2])), y(w(f, p[3])));
        return j > f ? 1 : -1
    });
    return c
}
var I, ca = document.documentElement.style, N = void 0 !== ca.WebkitTransform && "-webkit-" || void 0 !== ca.MozTransform && "-moz-" || void 0 !== ca.transform && "";
I = {
    size: function (a, b) {
        return "width:" + a.toFixed(2) + "px;height:" + b.toFixed(2) + "px;margin-left:" + -(a / 2).toFixed(2) + "px;margin-top:" + -(b / 2).toFixed(2) + "px;"
    }, translate: function (a, b, c, e, g, f, j, p) {
        return N + "transform:translate3d(" + a.toFixed(2) + "px," + b.toFixed(2) + "px," + c.toFixed(2) + "px)rotateX(" + e.toFixed(2) + "deg)rotateY(" + g.toFixed(2) + "deg)rotateZ(" + f.toFixed(2) + "deg)skewX(" + (j || 0) + "deg)skewY(" + (p || 0) + "deg);"
    }, origin: function (a, b, c) {
        return N + "transform-origin:" + a.toFixed(2) + "px " + b.toFixed(2) + "px " + c.toFixed(2) +
        "px;"
    }, r: function (a) {
        return N + "perspective:" + a + "px;"
    }, D: function (a) {
        for (var b = 0, c = 0; c < ka.length; c++)
            var e = ka[c], e = y(u(e.l(), a)) / (100 + 10 * e.v), b = Math.max(b, 1 - e);
        return b
    }, C: function (a) {
        return a.i ? la(a.i) : ""
    }, j: function (a) {
        var b = [], c = [], e = [], g = I.C(a);
        "-" == g.charAt(0) ? b.push(g) : b.push("url(" + g + ")");
        c.push("cover");
        e.push("0 0");
        b.push("url(" + a.j.src + ")");
        c.push("auto");
        e.push((a.j.x || 0) + "px -" + (a.j.y || 0) + "px");
        b = "background:" + b.join() + ";background-position:" + e.join() + ";background-size: " + c.join() + ";";
        a.j.F && (b += N + "mask-image:url(" + a.j.src + ");" + N + "mask-position:" + e[1] + ";");
        return b
    }, f: function (a) {
        var b;
        a instanceof O && (b = new O);
        a instanceof G && (b = new G(a.J, a.width, a.height, a.position.x));
        if (b && b.e)
            for (var c = 0; c < a.e.length; c++)
                b.add(I.f(a.e[c]));
        return b
    }
};
function ma(a, b) {
    this.set(a, b)
}
ma.prototype = {
    set: function (a, b, c) {
        1 == arguments.length ? this.set(a.x, a.y) : (this.x = a || 0, this.y = b || 0)
    }
};
var na = 0, ka = [];
function T() {
    this.c = document.createElement("div");
    this.c.id = na++;
    this.position = r();
    this.rotation = r();
    this.o = new ma
}
T.prototype = {
    update: function () {
        this.c.style.cssText += this.h()
    }, h: function () {
        return I.translate(this.position.x, this.position.y, this.position.a, this.rotation.x, this.rotation.y, this.rotation.a, this.o.x, this.o.y)
    }, f: function () {
        return I.f(this)
    }, l: function () {
        for (var a = this, b = r(0, 0, 0) ; a;)
            b = u(b, a.position), a = a.parent;
        return b
    }
};
function F() {
    T.call(this);
    this.e = []
}
F.prototype = {
    update: function (a) {
        T.prototype.update.call(this);
        if (a)
            for (a = 0; a < this.e.length; a++)
                this.e[a].update(k)
    }, add: function (a, b, c, e, g, f, j, p, z) {
        a.parent && a.parent.remove(a);
        "undefined" != typeof b && (a.position.x = b);
        "undefined" != typeof c && (a.position.y = c);
        "undefined" != typeof e && (a.position.a = e);
        "undefined" != typeof g && (a.rotation.x = g);
        "undefined" != typeof f && (a.rotation.y = f);
        "undefined" != typeof j && (a.rotation.a = j);
        "undefined" != typeof p && (a.o.x = p);
        "undefined" != typeof z && (a.o.y = z);
        a.parent = this;
        this.e.push(a);
        this.c.appendChild(a.c);
        a instanceof U && ka.push(a);
        return a
    }, remove: function (a) {
        for (var b = 0; b < this.e.length; b++)
            if (this.e[b] === a)
                return a.parent = null, a.c.parentNode.removeChild(a.c), this.e.splice(b, 1), a
    }, f: T.prototype.f, h: T.prototype.h
};
function U(a, b, c) {
    T.call(this);
    this.v = a;
    this.w = b;
    this.color = c;
    this.c.className = "light"
}
U.prototype = { h: T.prototype.h, f: T.prototype.f, update: T.prototype.update, l: T.prototype.l };
function G(a, b, c, e) {
    T.call(this);
    this.j = a;
    this.width = b;
    this.height = c;
    this.B = e !== q;
    this.c.className = "plane"
}
G.prototype = {
    h: function () {
        return I.size(this.width, this.height) + I.j(this) + T.prototype.h.apply(this)
    }, f: T.prototype.f, update: T.prototype.update
};
function ba(a) {
    var b, c = a.width / 2 | 0;
    b = a.height / 2 | 0;
    for (var e = r(-c, -b, 0), g = r(+c, -b, 0), f = r(+c, +b, 0), c = r(-c, +b, 0), j = a; j;)
        a = j.rotation, b = j.position, e = u(A(e, a), b), g = u(A(g, a), b), f = u(A(f, a), b), c = u(A(c, a), b), j = j.parent;
    return [e, g, f, c]
}
function O() {
    F.call(this);
    this.c.className = "assembly"
}
O.prototype = F.prototype;
function oa(a, b) {
    this.c = document.createElement("div");
    this.c.className = "viewport";
    this.n = document.createElement("div");
    this.n.className = "camera";
    this.g = { position: r(0, 0, 0), rotation: r(0, 0, 0) };
    this.m = new O;
    this.c.appendChild(this.n);
    this.n.appendChild(this.m.c);
    a.appendChild(this.c);
    var c = b || 700;
    this.r = c;
    this.c.style.cssText = I.r(c)
}
oa.prototype = {
    update: function () {
        this.n.style.cssText = I.translate(0, 0, this.r, this.g.rotation.x, this.g.rotation.y, this.g.rotation.a);
        this.m.c.style.cssText = I.translate(this.g.position.x, this.g.position.y, this.g.position.a, 0, 0, 0)
    }
};
var la, V = document.createElement("canvas"), pa = V.getContext("2d");
la = function (a) {
    var b = a.length, c = a[0].length, e = pa.createImageData(c, b);
    V.height = e.height;
    V.width = e.width;
    for (var g = 0, f = e.data, j = 0; j < b; j++)
        for (var p = 0; p < c; p++)
            f[g] = 0, f[g + 1] = 0, f[g + 2] = 0, f[g + 3] = 255 - a[j][p], g += 4;
    pa.putImageData(e, 0, 0);
    return V.toDataURL()
};
function qa() {
    this.canvas = document.createElement("canvas");
    this.t = this.canvas.getContext("2d")
}
qa.prototype = {};
function Aa(a, b, c) {
    return a.u ? (c = 29 - (c /= 24) | 0, b = 46 + (b /= 24) | 0, a = a.t.getImageData(c, b, 1, 1), 25 * (255 - a.data[0])) : 0
}
for (var Ba = 0, W = ["ms", "moz", "webkit", "o"], $ = 0; $ < W.length && !window.requestAnimationFrame; ++$)
    window.requestAnimationFrame = window[W[$] + "RequestAnimationFrame"], window.cancelAnimationFrame = window[W[$] + "CancelAnimationFrame"] || window[W[$] + "CancelRequestAnimationFrame"];
window.requestAnimationFrame || (window.requestAnimationFrame = function (a) {
    var b = (new Date).getTime(), c = Math.max(0, 16 - (b - Ba)), e = window.setTimeout(function () {
        a(b + c)
    }, c);
    Ba = b + c;
    return e
});
window.cancelAnimationFrame || (window.cancelAnimationFrame = function (a) {
    clearTimeout(a)
});
window.onload = function () {
    function a() {
        O.call(this);
        this.add(new c({ src: ra }, 100, 196, 20));
        this.add(new G({ src: ra, x: 0, y: 196, I: 100, K: 100, F: k }, 100, 100, q), 0, -98, 0, 90)
    }
    function b() {
        e.call(this, { src: Ca }, 150, 150, 150)
    }
    function c(a, b, c, d) {
        O.call(this);
        for (var f = 2 * (Math.PI / d), e = b * Math.sin(Math.PI / d), g = 0; g < d; g++) {
            var h = Math.sin(f * g) * b / 2, j = Math.cos(f * g) * b / 2, l = Math.atan2(h, j);
            this.add(new G({ src: a.src, x: e * (d - g), I: e + 1 }, e + 1, c), h, 0, j, 0, 57.2957795 * l, 0)
        }
    }
    function e(a, b, c, d) {
        O.call(this);
        this.add(new G({ src: a.src }, b,
        c), 0, 0, d / 2, 0, 0, 0);
        this.add(new G({ src: a.src }, b, c), 0, 0, -d / 2, 0, 180, 0);
        this.add(new G({ src: a.src }, d, c), b / 2, 0, 0, 0, 90, 0);
        this.add(new G({ src: a.src }, d, c), -b / 2, 0, 0, 0, 270, 0);
        this.add(new G({ src: a.src }, b, d), 0, -c / 2, 0, 90, 0, 0);
        this.add(new G({ src: a.src }, b, d), 0, c / 2, 0, 270, 0, 0)
    }
    function g() {
        O.call(this)
    }
    function f() {
        setTimeout(function () {
            sa.className = "";
            ta.className = "";
            ua()
        }, 1500)
    }
    var j, p, z, s, t;
    function va(a) {
        var b = "keydown" == a.type;
        switch (a.keyCode) {
            case 87:
                j = b;
                break;
            case 83:
                p = b;
                break;
            case 65:
                z = b;
                break;
            case 68:
                s =
                b;
                break;
            case 32:
                t = b
        }
    }
    function ua(a) {
        a = a - Da || 0;
        requestAnimationFrame(ua);
        s ? d.b = Math.min(d.b + D, X) : z ? d.b = Math.max(d.b - D, -X) : 0 < d.b ? d.b = Math.max(d.b - D, 0) : 0 > d.b && (d.b = Math.min(d.b + D, 0));
        d.d = j ? Math.min(d.d + D, X) : p ? Math.max(d.d - D, -X) : 0 < d.d ? Math.max(d.d - D, 0) : 0 > d.d ? Math.min(d.d + D, 0) : 0;
        var b = 0.0174532925 * J.g.rotation.y, c = d.position.x - Math.sin(b) * d.d - Math.cos(b) * d.b, b = d.position.a + Math.cos(b) * d.d - Math.sin(b) * d.b, e = Aa(da, c, b);
        if ((0 !== d.d || 0 !== d.b || 0 !== d.k) && wa)
            sa.className = "hide", wa = q;
        60 < e - d.position.y ? (d.d = 0,
        d.b = 0) : (d.position.x = c, d.position.a = b);
        c = Aa(da, d.position.x, d.position.a);
        d.position.y > c ? d.position.y < c + 9 ? (d.position.y = c, d.b /= 1.3, d.d /= 1.3) : d.k = Math.min(40, Math.max(-12, d.k + 0.75)) : d.position.y > c - 9 ? d.position.y = c : (d.k = -5, d.d /= 1.1, d.b /= 1.1);
        d.position.y === c && (d.k = 0, t && (d.k = -12));
        a = Math.cos(a / 250) * d.d / 15;
        d.position.y -= d.k;
        E.rotation = d.rotation;
        E.rotation.x += a;
        E.position.x = d.position.x;
        E.position.y = d.position.y + 275;
        E.position.a = d.position.a;
        J.update();
        E.rotation.x -= a;
        a = (1 - 2 * I.D(d.position)).toFixed(2);
        Ea.style.cssText = "background:-webkit-linear-gradient(rgba(0,0,0," + a + "),rgba(0,0,0," + a + ")),url(assets/CSS3FPSByKeith/gun.png);-webkit-mask-image:url(assets/CSS3FPSByKeith/gun.png);"
    }
    var ta = document.getElementById("viewport"), sa = document.getElementById("instructions"), X = 5, D = 0.2, da = new qa, wa = k, J = new oa(ta), E = J.g;
    t = s = z = p = j = q;
    var ea = 0, fa = 0, P = da, Q = new Image;
    P.u = q;
    Q.onload = function () {
        var a = Q.width, b = Q.height;
        P.u = k;
        P.canvas.width = a;
        P.canvas.height = b;
        P.t.drawImage(Q, 0, 0);
        f && f()
    };
    Q.src = "assets/CSS3FPSByKeith/map.png";
    document.addEventListener("mouseover",
    function (a) {
        ea = a.pageX;
        fa = a.pageY;
        document.removeEventListener("mouseover", arguments.callee)
    }, q);
    document.addEventListener("mousemove", function (a) {
        d.rotation.x -= (a.pageY - fa) / 2;
        d.rotation.y += (a.pageX - ea) / 2;
        d.rotation.x = Math.max(Math.min(d.rotation.x, 90), -90);
        ea = a.pageX;
        fa = a.pageY
    }, q);
    document.addEventListener("keydown", va, q);
    document.addEventListener("keyup", va, q);
    var d = { position: r(-1E3, 500, -1050), rotation: r(0, 270, 0), d: 0, k: 0, b: 0 }, Ea = document.getElementById("gun"), R = J.m, Ca = "assets/CSS3FPSByKeith/crate.jpg", ra = "assets/CSS3FPSByKeith/drum.png";
    g.prototype = new O;
    e.prototype = new O;
    c.prototype = new O;
    b.prototype = new O;
    a.prototype = new O;
    var h = new g;
    h.add(new G({ src: "assets/CSS3FPSByKeith/wall.jpg" }, 2E3, 700), 0, -350, -700, 0, 0, 0);
    h.add(new G({ src: "assets/CSS3FPSByKeith/wall.jpg" }, 1E3, 700), 500, -350, 300, 0, 180, 0);
    h.add(new G({ src: "assets/CSS3FPSByKeith/wall.jpg" }, 1E3, 700), 1E3, -350, -200, 180, 270, 0);
    h.add(new G({ src: "assets/CSS3FPSByKeith/wall.jpg" }, 500, 1100), 0, -550, 550, 0, 270, 0);
    h.add(new G({ src: "assets/CSS3FPSByKeith/wall.jpg" }, 600, 700), -700, -350, 300, 0, 180, 0);
    h.add(new G({ src: "assets/CSS3FPSByKeith/wall.jpg" }, 1E3, 700), -1E3, -350,
    -200, 0, 90, 0);
    h.add(new G({ src: "assets/CSS3FPSByKeith/floor.jpg" }, 2E3, 1500), 0, 0, 0, 90, 0, 0);
    h.add(new G({ src: "assets/CSS3FPSByKeith/ceil.jpg" }, 2E3, 1E3), 0, -700, -200, 270, 0, 0);
    h.add(new c({ src: "assets/CSS3FPSByKeith/pipe.jpg" }, 60, 2E3, 26), 0, -610, 180, 0, 0, 90);
    h.add(new c({ src: "assets/CSS3FPSByKeith/pipe.jpg" }, 40, 700, 20), 940, -350, -500, 0, 180, 0);
    h.add(new c({ src: "assets/CSS3FPSByKeith/pipe.jpg" }, 40, 700, 20), 940, -350, -420, 0, 180, 0);
    h.add(new b, -800, -75, -470);
    h.add(new b, -820, -225, -380, 0, 15);
    h.add(new b, -850, -75, -270, 0, -15);
    h.add(new a, -300, -98, -350);
    h.add(new a, 350, -98, 110, 0,
    90);
    h.add(new a, 250, -98, -510);
    h.add(new a, 640, -98, -440, 0, 190, 0);
    h.add(new a, 90, -98, -20, 0, 120, 0);
    h.add(new U(65, 0.5, { s: 0, q: 255, p: 0 }), 440, -500, -200);
    h.add(new U(55, 0.8, { s: 0, q: 0, p: 255 }), -520, -450, -200);
    h.add(new U(40, 0.25, { s: 255, q: 0, p: 0 }), -210, -520, 400);
    h.add(new G({ src: "assets/CSS3FPSByKeith/wall.jpg" }, 400, 400, q), -200, -900, 300, 0, 0, 0);
    h.add(new G({ src: "assets/CSS3FPSByKeith/wall.jpg" }, 1E3, 1100), -400, -550, 800, 0, 90, 0);
    for (var m = 0; 10 > m; m++)
        h.add(new G({ src: "assets/CSS3FPSByKeith/floor.jpg", y: 100 * m }, 400, 50, q), -201, 50 * -m - 25, 50 * m + 300, 0, 180), h.add(new G({
            src: "assets/CSS3FPSByKeith/floor.jpg",
            y: 100 * m + 50
        }, 400, 50, q), -201, 50 * -m - 50, 50 * m + 325, 90);
    m = new g;
    m.add(new G({ src: "assets/CSS3FPSByKeith/floor.jpg" }, 1400, 500), -200, 0, 0, 90, 0, 0);
    m.add(new G({ src: "assets/CSS3FPSByKeith/wall.jpg" }, 1E3, 600), 0, -300, -250, 0, 0, 0);
    m.add(new G({ src: "assets/CSS3FPSByKeith/wall.jpg" }, 1400, 600), -200, -300, 250, 0, 180, 0);
    m.add(new G({ src: "assets/CSS3FPSByKeith/ceil.jpg" }, 1400, 1E3), -200, -600, -250, 270, 0, 0);
    m.add(new G({ src: "assets/CSS3FPSByKeith/wall.jpg" }, 500, 600), 500, -300, 0, 0, 270, 0);
    m.add(new b, -600, -75, 140, 0, 5);
    m.add(new a, -100, -98, -120, 0, 0);
    m.add(new U(80, 0.45, { s: 0, q: 0, p: 255 }), -80,
    -450, 150);
    R.add(h, 100, 0, 0);
    R.add(m, 600, -500, 1050);
    R = B(J.m, U);
    for (h = 0; h < R.length; h++)
        for (var m = R[h], ga = [], xa = aa(m.parent, m), ha = 0; ha < xa.length; ha++) {
            plane = xa[ha];
            plane.i || (plane.i = []);
            points = ba(plane);
            for (var n = w(points[1], points[0]), l = w(points[2], points[1]), K = x({ x: n.y * l.a - n.a * l.y, y: n.a * l.x - n.x * l.a, a: n.x * l.y - n.y * l.x }, y({ x: n.y * l.a - n.a * l.y, y: n.a * l.x - n.x * l.a, a: n.x * l.y - n.y * l.x })), H = Math.ceil(plane.width / 12), S = Math.ceil(plane.height / 12), n = x(n, H), ia = x(l, S), L = u(points[0], x(w(points[2], points[0]), 2)), l = m.l(),
            v = w(L, l), L = K.x * x(v, y(v)).x + K.y * x(v, y(v)).y + K.a * x(v, y(v)).a, K = 1 - Math.max(0, L), L = K * Math.acos(L) * m.w, C = 0; C < S; C++) {
                plane.i[C] || (plane.i[C] = []);
                for (var ja = u(points[0], { x: ia.x * C, y: ia.y * C, a: ia.a * C }), Y = 0; Y < H; Y++) {
                    for (var ja = u(ja, n), v = w(ja, l), M = y(v), Fa = 1 - M / (100 + 10 * m.v), ya = q, za = Math.atan2(v.a, -v.x), v = Math.asin(-v.y / M), M = 0; M < ga.length; M++) {
                        var Z = ga[M];
                        if (za >= Z.G && za <= Z.z && v >= Z.H && v <= Z.A) {
                            ya = k;
                            break
                        }
                    }
                    amt = ya ? 0 : 255 * Math.min(1, Math.max(0, L * Fa));
                    plane.i[C][Y] = Math.max(plane.i[C][Y] || 0, amt)
                }
            }
            0.75 > K && plane.B && (n = w(l,
            points[0]), l = w(l, points[2]), H = -Math.atan2(n.a, n.x), S = -Math.atan2(l.a, l.x), n = Math.asin((n.y - 7) / y(n)), l = Math.asin((l.y - 7) / y(l)), H = { G: Math.min(H, S), H: Math.min(n, l), z: Math.max(H, S), A: Math.max(n, l) }, ga.push(H))
        }
    J.m.update(k);
    var Da = new Date
};
