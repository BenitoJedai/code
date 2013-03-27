for (G = [[0, j = 270], [0, -j]]; j--;)
    for (G.push([60 + Math.sin(i = j / 8) * 3, (50 + Math.cos(i) * 3 + i * i / 37) * Math.cos(Math.PI * j), i & 32 ? 70 : 16, -80 - i * 2]), b = a[j] = c.cloneNode(i = 270), e = b.getContext('2d'), b.width = b.height = 60; x = j / 8, e.fillStyle = 'hsla(' + [(j & 15) * 8 - x, (j & 15) * 6 + x + '%', (j < 17) * 60 + (j & 15) * 7 + '%', 1] + ')', i--;)
        for (; j / 16 < x--;)
            e.fillRect(30 + x * Math.cos(i) * Math.cos(i * (y = Math.sin(i))), 30 + x * y, .07, 1);
for (setInterval(function (b, e) {
    for (c.height = i = e = 560; a.fillStyle = 'hsla(' + [e, e + '%', 73 + '%', i / 601] + ')', i -= 7, 17 < i--;)
        a.fillRect(0, e - i, 1e3, -8);
    for (j++; i--;)
        e = 8e3 / ((i + j / 50) % 30), a.drawImage(a[95], 500 - (.5 + Math.cos(i) * .5) * e * 5, 660 - e, e * 5, e);
    G.sort(function (b, e) {
        return b[0] - e[0]
});
    G.map(function (b, e) {
        b[0] = Math.cos(i = Math.cos(j / 60) / 50) * b[0] - Math.sin(i) * [b[1], b[1] = Math.sin(i) * b[0] + Math.cos(i) * b[1]][0];
        y = Math.cos(j / 60 + Math.cos(j / 16)) * (60 + b[0] / 6) + 270;
        x = 500 - i * 8e3;
        for (a.drawImage(a[~~b[2]], b[1] - 30 + x - ~b[4] / 150 * Math.cos(j / 5 + b[0] / 16), b[3] - 30 + y), i = 50; !b[2] && i--;)
            a.beginPath(), a.moveTo(-b[0] / 8 + x + b[1] / 2, y), a.fill(a.bezierCurveTo(-b[0] + x + b[1], Math.cos(Math.cos(i) * j) * 270 + y, b[0] + x + b[1], Math.cos(Math.cos(i) * j) * 270 + y, b[0] / 8 + x + b[1] / 2, y))
})
}, j = 4) ; j--;)
    for (i = j ? c.width = 1e3 : 9e3; z = Math.cos(i) * Math.cos(i * (y = Math.sin(i))), x = Math.cos(i) * Math.sin(i * y), i--;)
        j | (z + 1) % .5 < .1 && G.push([(z * (191 >> j * 4 & 15) + (e = 52736 >> j * 4 & 15)) * 8, x * (12700 >> j * 4 & 15) * 9 + Math.cos(Math.PI * i) * 3 * e, (59525 >> j * 4 & 15) - y * (5188 >> j * 4 & 15) + (.5 + Math.cos(i) * .5) * (50 >> j * 4 & 15) + (25080 >> j * 4 & 15) * 16, y * (12700 >> j * 4 & 15) * 8 - (j & 2) * 16]), j || Math.abs(e = 8 - i % 16) < 54 - i / 16 && G.push([50 - i * i / 2e4, e + e * Math.sin(i / 270), 16 | [1, 14, 1, , , 1, 8, 1][~~(i / 48 + e / 3) % 10], 90 + i / 8, i])
