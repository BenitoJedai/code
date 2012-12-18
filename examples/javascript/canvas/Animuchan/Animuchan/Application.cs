using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Animuchan.Design;
using Animuchan.HTML.Pages;

namespace Animuchan
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {

            // by http://animuchan.net/

            new IFunction(@"
var $c = {}, $n = 0xc0

function $(id) {return $c[id] || ($c[id] = document.getElementById(id))}

$c.put = function(id, parent) {
	var obj = $c[id] = document.createElement('canvas')
	obj.id = id
	obj.width = obj.height = $n
	return parent.appendChild(obj)
}

if (typeof requestAnimationFrame == 'undefined') {
	for (var p in {moz:1, webkit:1, o:1, ms:1}) {
		if (typeof window[p + 'RequestAnimationFrame'] != 'undefined') {
			requestAnimationFrame = window[p + 'RequestAnimationFrame']
		}
	}
}

if (typeof requestAnimationFrame == 'undefined') {
	requestAnimationFrame = function(fun) {setTimeout(fun, 16)}
}

function chain(fun) {
	return function() {return fun.apply(this, arguments) || this}
}



function render() {
	requestAnimationFrame(render)
	var now = +new Date(), i, j, k

	C.data[Math.floor(Math.random() * 8 + 5) % 9] = Math.floor(Math.random() * 3)

	C.foo.
		set('fillStyle', 'rgba(0,0,0,0.1)').
		fillRect(0, 0, $n, $n).
		set('fillStyle', '#653')
	C.bar.
		clearRect(0, 0, $n, $n)

	for (i = -1; k = (C.data[++i] & 1) * 62, i < 9;
		C.foo.fillRect((i % 3) * 64 + 1, Math.floor(i / 3) * 64 + 1, k, k));

	for (j = 0; ++j < 4; C.foo.drawImage(C.bar.canvas, 0, 0)) {
		for (i = -1; k = (C.data[++i] & 2) * 31, i < 9;
			C.bar.drawImage(C.foo.canvas, 0, 0, $n, $n,
				(i % 3) * 64 + 1, Math.floor(i / 3) * 64 + 1, k, k));
	}

	C.logic.
		clearRect(0, 0, $n, $n).
		save().
		translate(96, 96).
		rotate((now / 5841 % 2) * Math.PI).
		uniscale((now / 1274 % 1) * 2 + 1).
		drawCanvas(C.foo, 0, $n, -288, 576).
		drawCanvas(C.foo, 0, $n, -96, $n).
		drawCanvas(C.foo, 0, $n, -32, 64).
		restore()
	C.render.
		set('globalCompositeOperation', 'source-over').
		clearRect(0, 0, $n, $n).
		drawImage(C.logic.canvas, 0, 0).
		set('globalCompositeOperation', 'lighter')

	for (i = -1; k = 96 >> ++i, i < 6;
		C.render.drawCanvas(C.logic.drawCanvas(C.logic, 0, k << 1, 0, k), 0, k, 0, $n));
}

function init() {
	C = {data: []}
	C.data[4] = 2

	'main nyan ninja'.replace(/[^ ]+/g, $)

	for (var canvas in {foo:1, bar:1, logic:1, render:1}) {
		C[canvas] = $c.put(canvas, $c.ninja).getContext('2d')
	}

//	$c.main.replaceChild($c.render, $c.nyan)

	CanvasRenderingContext2D.prototype.set = function(what, to) {
		this[what] = to
		return this
	}

	CanvasRenderingContext2D.prototype.uniscale = function(factor) {
		this.scale(factor, factor)
		return this
	}

	CanvasRenderingContext2D.prototype.drawCanvas = function(obj, a, s, d, f) {
		this.drawImage(obj.canvas, a, a, s, s, d, d, f, f)
		return this
	}

	for (var fun in {clearRect:1, drawImage:1, fillRect:1, rotate:1, save:1, translate:1}) {
		CanvasRenderingContext2D.prototype[fun] = chain(CanvasRenderingContext2D.prototype[fun])
	}

	requestAnimationFrame(render)
}

init();
").apply(null);

        }

    }
}
