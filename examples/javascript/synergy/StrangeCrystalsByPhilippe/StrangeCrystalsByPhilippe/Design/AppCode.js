b.bgColor = X = Y = H = I = J = E = Q = U = V = 0;
T = [l = 1];
C = (c.width = c.height = innerHeight) / 2;
setInterval(function () {
    for (i = H; i < X + 1400; H = ++i) {
        (s = i % 800) || (D = i / 800, P = D & 1 && -Q + (Q = Math.random() > .5), K = !P && D && (K + 2 + (Math.random() > .5)) % 3 - 1, B = !P && D > 9 && !B && Math.random() > .7, W = 1.8 * Math.random() - 1);
        E += F = (s < 160) - (s > 639);
        p = T[i % 1400] = [I += J, J += K, U += V, V = P * E, !P && Math.exp(-(L = i % 400 - 200) * L / 3e3) / 1.5, i % 400, Math.random() > .95];
        if (i % 10 == 0) {
            for (y = Z = 9.8 + B * E / 15, z = W * B * E / 15, g = Math.random() / 6; g < 6.3; g += 2 / Z)
                Math.random() > (P || B ? .4 : Math.cos(g) > .5) && p.push(1.1 * Z * Math.cos(g + 11), 7 + B * 16, 4 - F, Z * Math.cos(g) + 2 + z, 4 - F);
            for (t = 2 * Z * Math.random() - Z, w = t * t * Math.random() / 6; B * w > .2;)
                p.push(t, 7 + B * 16, w, --y + z, 1.1, t, 7 + B * 16, w *= .8, -y + z, 1.1);
            Q || B * W > .6 && p.push(2 * Z * Math.random() - Z, 12, .2, Z * Math.random() - Z / 2 + z, 1 + Math.random());
            !B || i % 20 || p.push(-2, 22, .5, -7, Z - 8 - z, 2, 22, .5, -7, Z - 8 - z);
            i % 20 || p.push(0, 6, 6, -6.8, .8);
            L || p.push(0, 7 + B * 16, .2, Z + z, Z + z - 5, 0, 7, .6, 5, .6);
            P || B || (i % 40 || p.push(-6, 6, 1.5, 5, 14, 6, 6, 1.5, 5, 14), i % 20 || p.push(0, 6, 17, 8, 3))
        }
        i % 5 || p.push(-2, 6, .5, -6.5, .6, 2, 6, .5, -6.5, .6)
    }
    for (q = T[X % 1400]; d = --i - X;)
        for (p = T[i % 1400], f = C / (d / 10 + 3), v = g = 0, t = C + f * (p[g] - q[g++] - d * q[g++]) / 3e3, u = C + f * (p[g] - q[g++] - d * q[g++]) / 3e3, x = p[g++] * (l = p[g++] ? g++ && l : p[g++] ? Math.random() > .3 : 1), y = d / 1800; z = f * p[g++], w = p[g++];) {
            v != w && (v = w, a.fillStyle = 'hsl(0,0%,' + 16 * (w & 7) * (w & 8 || y + x * (w < 16)) + '%)');
            a.fillRect(t + z - (z = f * p[g++]) / 2, u - f * p[g++] + (w == 12 && f * 6 * Math.cos(g + X / 400)), z, f * p[g++])
        }
    Y += 4 - 3 * Math.exp(-X / 400) + q[2] / 3e4;
    X = 0 | Y
}, 35)