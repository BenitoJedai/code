(function (o) {
    var g = function () {
        throw "Synchronous require() is not supported.";
    };
    g.unit = {};
    var p = "",
        j, k, q, r = 2E4,
        m, C = document.getElementsByTagName("head")[0],
        l = Object.prototype.hasOwnProperty;
    if (function () {
        for (var a in {
            hasOwnProperty: !0
        }) return a
    }() == "hasOwnProperty") var s = function (a, b, d) {
        for (var c in a) l.call(a, c) && b.call(d, c)
    };
    else var y = ["isPrototypeOf", "hasOwnProperty", "toLocaleString", "toString", "valueOf"],
        s = function (a, b, d) {
            for (var c in a) l.call(a, c) && b.call(d, c);
            for (var e = y.length; e--;) c = y[e],
            l.call(a, c) && b.call(d, c)
        };
    var D = function (a, b) {
        for (var d = 0; d < a.length;) b.call(null, a[d], d) === !0 ? a.splice(d, 1) : d++
    }, z = function (a, b) {
        var d = a.split("/"),
            b = b || "";
        b.length && b.charAt(b.length - 1) != "/" && (b += "/");
        var c = b.split("/");
        c.pop();
        for (var e; e = d.shift();) e != "." && (e == ".." && c.length && c[c.length - 1] != ".." ? c.pop() : c.push(e));
        return c.join("/")
    }, n = g.unit.resolveModuleId = function (a, b) {
        return a.charAt(0) != "." ? a : z(a, b)
    }, t = function (a) {
        return a.charAt(0) != "." ? p + a + ".js" : this._resolveModuleId(a, p) + ".js"
    }, i = function (a) {
        if (!l.call(j,
        a)) return null;
        return j[a]
    }, E = function (a, b) {
        k.push([a.slice(0), b])
    }, B = function (a, b, d) {
        for (var c = [], e = a.length; e--;) {
            var h = n(a[e], d);
            i(h);
            u(h) || c.push(h)
        }
        c.length ? (E(c, function () {
            b(v(d))
        }), A(c)) : setTimeout(function () {
            b(v(d))
        }, 0)
    }, v = function (a) {
        var b = function (b) {
            var c = n(b, a);
            if (b = i(c)) {
                if (b.error) throw "Error loading module";
            } else throw "Module not loaded";
            if (!b.exports) {
                b.exports = {};
                for (var c = c.substring(0, c.lastIndexOf("/") + 1), e = b.injects, h = [], f = 0, w = e.length; f < w; f++) e[f] == "require" ? h.push(v(c)) : e[f] ==
                    "exports" ? h.push(b.exports) : e[f] == "module" && h.push(b.module);
                b.factory.apply(null, h)
            }
            return b.exports
        };
        b.ensure = function (b, c) {
            B(b, c, a)
        };
        if (m != null) b.main = i(m).module;
        return b
    }, A = function (a) {
        for (var b = a.length; b--;) {
            var d = a[b];
            i(d) == null && (j[d] = {}, q(d))
        }
    }, u = function (a) {
        var b = {}, d = function (a) {
            if (b[a] == !0) return !0;
            b[a] = !0;
            a = i(a);
            if (!a || !a.defined) return !1;
            else {
                for (var a = a.deps || [], e = a.length; e--;) if (!d(a[e])) return !1;
                return !0
            }
        };
        return d(a)
    }, x = function () {
        for (var a = 0; a < k.length;) {
            for (var b = k[a][0], d = k[a][1],
            c = 0; c < b.length;) u(b[c]) ? b.splice(c, 1) : c++;
            b.length ? a++ : (k.splice(a, 1), d != null && setTimeout(d, 0))
        }
    };
    q = function (a) {
        var b, d = function () {
            var b = i(a);
            if (!b.defined) b.defined = b.error = !0, x()
        }, c = window.XMLHttpRequest ? new XMLHttpRequest : new ActiveXObject("Microsoft.XMLHTTP"),
            e = t(a);
        c.open("GET", e, !0);
        c.onreadystatechange = function () {
            if (c.readyState === 4) if (clearTimeout(b), c.status == 200 || c.status === 0) {
                for (var h = c.responseText, f = F(h), w = a.substring(0, a.lastIndexOf("/") + 1), i = {}, k = f.length; k--;) f[k] = n(f[k], w);
                try {
                    i[a] = o("({fn: function(require, exports, module) {\r\n" + h + "\r\n}})").fn
                } catch (j) {
                    throw j instanceof SyntaxError && (h = "Syntax Error: ", j.lineNumber ? h += "line " + (j.lineNumber - 581) : console.log("GameJs tip: use Firefox to see line numbers in Syntax Errors."), h += " in file " + e, console.log(h)), j;
                }
                g.define(i, f)
            } else d()
        };
        b = setTimeout(d, r);
        c.send(null)
    };
    var F = g.unit.determineShallowDependencies = function (a) {
        for (var b = {}, d, c = /(?:^|[^\w\$_.])require\s*\(\s*("[^"\\]*(?:\\.[^"\\]*)*"|'[^'\\]*(?:\\.[^'\\]*)*')\s*\)/g; d = c.exec(a);) d = eval(d[1]), l.call(b, d) || (b[d] = !0);
        for (c = /(?:^|[^\w\$_.])require.ensure\s*\(\s*(\[("[^"\\]*(?:\\.[^"\\]*)*"|'[^'\\]*(?:\\.[^'\\]*)*'|\s*|,)*\])/g; d = c.exec(a);) for (var e = eval(d[1]), h = e.length; h--;) d = e[h], delete b[d];
        var f = [];
        s(b, function (a) {
            f.push(a)
        });
        return f
    }, G = function (a) {
        var b = document.createElement("script");
        b.type = "text/javascript";
        b.src = t(a);
        var d = !! b.addEventListener,
            c, e = function () {
                f(!1)
            }, h = function () {
                (d || b.readyState == "complete" || b.readyState == "loaded") && f(i(a).defined)
            }, f = function (f) {
                clearTimeout(c);
                d ? (b.removeEventListener("load", h, !1), b.removeEventListener("error", e, !1)) : b.detachEvent("onreadystatechange", h);
                if (!f && (f = i(a), !f.defined)) f.defined = f.error = !0, x()
            };
        d ? (b.addEventListener("load", h, !1), b.addEventListener("error", e, !1)) : b.attachEvent("onreadystatechange", h);
        c = setTimeout(e, r);
        C.appendChild(b)
    }, H = function (a, b, d) {
        var c = {
            modules: []
        }, e = ["require", "exports", "module"];
        typeof a == "object" ? (c.deps = b || [], s(a, function (b) {
            var d = {
                id: b
            };
            typeof a[b] == "function" ? (d.factory = a[b], d.injects = e) : (d.factory = a[b].factory, d.injects = a[b].injects || e);
            c.modules.push(d)
        })) : (c.deps = b.slice(0), D(c.deps, function (a) {
            a: {
                for (var b = e.length; b--;) if (e[b] == a) {
                    a = b;
                    break a
                }
                a = -1
            }
            return a >= 0
        }), c.modules.push({
            id: a,
            factory: d,
            injects: b
        }));
        return c
    };
    g.setModuleRoot = function (a) {
        if (!/^http(s?):\/\//.test(a)) var b = window.location.href,
            b = b.substr(0, b.lastIndexOf("/") + 1),
            a = z(a, b);
        a.length && a.charAt(a.length - 1) != "/" && (a += "/");
        p = a
    };
    g.setTimeoutLength = function (a) {
        r = a
    };
    g.useScriptTags = function () {
        q = G
    };
    g.def = g.define = function () {
        for (var a = H.apply(null, arguments), b = [], d = [], c = a.deps, e = a.modules.length; e--;) {
            var h = a.modules[e],
                f = h.id,
                g = i(f);
            g || (g = j[f] = {});
            g.module = {
                id: f,
                uri: t(f)
            };
            g.defined = !0;
            g.deps = c.slice(0);
            g.injects = h.injects;
            g.factory = h.factory;
            d.push(g)
        }
        for (e = c.length; e--;) f = c[e], g = i(f), (!g || !u(f)) && b.push(f);
        b.length && setTimeout(function () {
            A(b)
        }, 0);
        x()
    };
    g.isKnown = function (a) {
        return i(a) != null
    };
    g.isDefined = function (a) {
        a = i(a);
        return !(!a || !a.defined)
    };
    g.ensure = function (a, b) {
        B(a, b, "")
    };
    g.run = function (a, b) {
        a = m = n(a, "");
        g.ensure([a],

        function (d) {
            d(a);
            b != null && b()
        })
    };
    g.reset = function () {
        m = null;
        j = {};
        k = [];
        g.define({
            system: function () {}
        })
    };
    g.reset();
    window.require = g
})(function (o) {
    with(window) return eval(o)
});
require.define({
    "Box2dWeb-2.1.a.3": function (y, i) {
        var a = {};
        (function (a, c) {
            function g() {}
            if (!(Object.prototype.defineProperty instanceof Function) && Object.prototype.__defineGetter__ instanceof Function && Object.prototype.__defineSetter__ instanceof Function) Object.defineProperty = function (a, b, c) {
                c.get instanceof Function && a.__defineGetter__(b, c.get);
                c.set instanceof Function && a.__defineSetter__(b, c.set)
            };
            a.inherit = function (a, b) {
                g.prototype = b.prototype;
                a.prototype = new g;
                a.prototype.constructor = a
            };
            a.generateCallback = function (a, b) {
                return function () {
                    b.apply(a, arguments)
                }
            };
            a.NVector = function (a) {
                a === c && (a = 0);
                for (var b = Array(a || 0), g = 0; g < a; ++g) b[g] = 0;
                return b
            };
            a.is = function (a, b) {
                if (a === null) return !1;
                if (b instanceof Function && a instanceof b) return !0;
                if (a.constructor.__implements != c && a.constructor.__implements[b]) return !0;
                return !1
            };
            a.parseUInt = function (a) {
                return Math.abs(parseInt(a))
            }
        })(a);
        var s = Array,
            r = a.NVector;
        typeof a === "undefined" && (a = {});
        if (typeof a.Collision === "undefined") a.Collision = {};
        if (typeof a.Collision.Shapes ===
            "undefined") a.Collision.Shapes = {};
        if (typeof a.Common === "undefined") a.Common = {};
        if (typeof a.Common.Math === "undefined") a.Common.Math = {};
        if (typeof a.Dynamics === "undefined") a.Dynamics = {};
        if (typeof a.Dynamics.Contacts === "undefined") a.Dynamics.Contacts = {};
        if (typeof a.Dynamics.Controllers === "undefined") a.Dynamics.Controllers = {};
        if (typeof a.Dynamics.Joints === "undefined") a.Dynamics.Joints = {};
        (function () {
            function b() {
                b.b2AABB.apply(this, arguments)
            }
            function c() {
                c.b2Bound.apply(this, arguments)
            }
            function g() {
                g.b2BoundValues.apply(this,
                arguments);
                this.constructor === g && this.b2BoundValues.apply(this, arguments)
            }
            function p() {
                p.b2Collision.apply(this, arguments)
            }
            function f() {
                f.b2ContactID.apply(this, arguments);
                this.constructor === f && this.b2ContactID.apply(this, arguments)
            }
            function l() {
                l.b2ContactPoint.apply(this, arguments)
            }
            function m() {
                m.b2Distance.apply(this, arguments)
            }
            function d() {
                d.b2DistanceInput.apply(this, arguments)
            }
            function t() {
                t.b2DistanceOutput.apply(this, arguments)
            }
            function r() {
                r.b2DistanceProxy.apply(this, arguments)
            }
            function i() {
                i.b2DynamicTree.apply(this,
                arguments);
                this.constructor === i && this.b2DynamicTree.apply(this, arguments)
            }
            function s() {
                s.b2DynamicTreeBroadPhase.apply(this, arguments)
            }
            function I() {
                I.b2DynamicTreeNode.apply(this, arguments)
            }
            function w() {
                w.b2DynamicTreePair.apply(this, arguments)
            }
            function Q() {
                Q.b2Manifold.apply(this, arguments);
                this.constructor === Q && this.b2Manifold.apply(this, arguments)
            }
            function y() {
                y.b2ManifoldPoint.apply(this, arguments);
                this.constructor === y && this.b2ManifoldPoint.apply(this, arguments)
            }
            function o() {
                o.b2Point.apply(this,
                arguments)
            }
            function M() {
                M.b2RayCastInput.apply(this, arguments);
                this.constructor === M && this.b2RayCastInput.apply(this, arguments)
            }
            function D() {
                D.b2RayCastOutput.apply(this, arguments)
            }
            function N() {
                N.b2Segment.apply(this, arguments)
            }
            function J() {
                J.b2SeparationFunction.apply(this, arguments)
            }
            function K() {
                K.b2Simplex.apply(this, arguments);
                this.constructor === K && this.b2Simplex.apply(this, arguments)
            }
            function C() {
                C.b2SimplexCache.apply(this, arguments)
            }
            function O() {
                O.b2SimplexVertex.apply(this, arguments)
            }
            function E() {
                E.b2TimeOfImpact.apply(this,
                arguments)
            }
            function H() {
                H.b2TOIInput.apply(this, arguments)
            }
            function P() {
                P.b2WorldManifold.apply(this, arguments);
                this.constructor === P && this.b2WorldManifold.apply(this, arguments)
            }
            function L() {
                L.ClipVertex.apply(this, arguments)
            }
            function j() {
                j.Features.apply(this, arguments)
            }
            function n() {
                n.b2CircleShape.apply(this, arguments);
                this.constructor === n && this.b2CircleShape.apply(this, arguments)
            }
            function q() {
                q.b2EdgeChainDef.apply(this, arguments);
                this.constructor === q && this.b2EdgeChainDef.apply(this, arguments)
            }

            function h() {
                h.b2EdgeShape.apply(this, arguments);
                this.constructor === h && this.b2EdgeShape.apply(this, arguments)
            }
            function u() {
                u.b2MassData.apply(this, arguments)
            }
            function G() {
                G.b2PolygonShape.apply(this, arguments);
                this.constructor === G && this.b2PolygonShape.apply(this, arguments)
            }
            function x() {
                x.b2Shape.apply(this, arguments);
                this.constructor === x && this.b2Shape.apply(this, arguments)
            }
            function e() {
                e.b2Color.apply(this, arguments);
                this.constructor === e && this.b2Color.apply(this, arguments)
            }
            function k() {
                k.b2Settings.apply(this,
                arguments)
            }
            function v() {
                v.b2Mat22.apply(this, arguments);
                this.constructor === v && this.b2Mat22.apply(this, arguments)
            }
            function S() {
                S.b2Mat33.apply(this, arguments);
                this.constructor === S && this.b2Mat33.apply(this, arguments)
            }
            function Da() {
                Da.b2Math.apply(this, arguments)
            }
            function Ea() {
                Ea.b2Sweep.apply(this, arguments)
            }
            function T() {
                T.b2Transform.apply(this, arguments);
                this.constructor === T && this.b2Transform.apply(this, arguments)
            }
            function U() {
                U.b2Vec2.apply(this, arguments);
                this.constructor === U && this.b2Vec2.apply(this,
                arguments)
            }
            function V() {
                V.b2Vec3.apply(this, arguments);
                this.constructor === V && this.b2Vec3.apply(this, arguments)
            }
            function W() {
                W.b2Body.apply(this, arguments);
                this.constructor === W && this.b2Body.apply(this, arguments)
            }
            function X() {
                X.b2BodyDef.apply(this, arguments);
                this.constructor === X && this.b2BodyDef.apply(this, arguments)
            }
            function Fa() {
                Fa.b2ContactFilter.apply(this, arguments)
            }
            function Ga() {
                Ga.b2ContactImpulse.apply(this, arguments)
            }
            function Ha() {
                Ha.b2ContactListener.apply(this, arguments)
            }
            function Y() {
                Y.b2ContactManager.apply(this,
                arguments);
                this.constructor === Y && this.b2ContactManager.apply(this, arguments)
            }
            function Z() {
                Z.b2DebugDraw.apply(this, arguments);
                this.constructor === Z && this.b2DebugDraw.apply(this, arguments)
            }
            function Ia() {
                Ia.b2DestructionListener.apply(this, arguments)
            }
            function Ja() {
                Ja.b2FilterData.apply(this, arguments)
            }
            function $() {
                $.b2Fixture.apply(this, arguments);
                this.constructor === $ && this.b2Fixture.apply(this, arguments)
            }
            function aa() {
                aa.b2FixtureDef.apply(this, arguments);
                this.constructor === aa && this.b2FixtureDef.apply(this,
                arguments)
            }
            function ba() {
                ba.b2Island.apply(this, arguments);
                this.constructor === ba && this.b2Island.apply(this, arguments)
            }
            function Ka() {
                Ka.b2TimeStep.apply(this, arguments)
            }
            function ca() {
                ca.b2World.apply(this, arguments);
                this.constructor === ca && this.b2World.apply(this, arguments)
            }
            function La() {
                La.b2CircleContact.apply(this, arguments)
            }
            function da() {
                da.b2Contact.apply(this, arguments);
                this.constructor === da && this.b2Contact.apply(this, arguments)
            }
            function ea() {
                ea.b2ContactConstraint.apply(this, arguments);
                this.constructor === ea && this.b2ContactConstraint.apply(this, arguments)
            }
            function Ma() {
                Ma.b2ContactConstraintPoint.apply(this, arguments)
            }
            function Na() {
                Na.b2ContactEdge.apply(this, arguments)
            }
            function fa() {
                fa.b2ContactFactory.apply(this, arguments);
                this.constructor === fa && this.b2ContactFactory.apply(this, arguments)
            }
            function Oa() {
                Oa.b2ContactRegister.apply(this, arguments)
            }
            function Pa() {
                Pa.b2ContactResult.apply(this, arguments)
            }
            function ga() {
                ga.b2ContactSolver.apply(this, arguments);
                this.constructor === ga && this.b2ContactSolver.apply(this,
                arguments)
            }
            function Qa() {
                Qa.b2EdgeAndCircleContact.apply(this, arguments)
            }
            function ha() {
                ha.b2NullContact.apply(this, arguments);
                this.constructor === ha && this.b2NullContact.apply(this, arguments)
            }
            function Ra() {
                Ra.b2PolyAndCircleContact.apply(this, arguments)
            }
            function Sa() {
                Sa.b2PolyAndEdgeContact.apply(this, arguments)
            }
            function Ta() {
                Ta.b2PolygonContact.apply(this, arguments)
            }
            function ia() {
                ia.b2PositionSolverManifold.apply(this, arguments);
                this.constructor === ia && this.b2PositionSolverManifold.apply(this, arguments)
            }

            function Ua() {
                Ua.b2BuoyancyController.apply(this, arguments)
            }
            function Va() {
                Va.b2ConstantAccelController.apply(this, arguments)
            }
            function Wa() {
                Wa.b2ConstantForceController.apply(this, arguments)
            }
            function Xa() {
                Xa.b2Controller.apply(this, arguments)
            }
            function Ya() {
                Ya.b2ControllerEdge.apply(this, arguments)
            }
            function Za() {
                Za.b2GravityController.apply(this, arguments)
            }
            function $a() {
                $a.b2TensorDampingController.apply(this, arguments)
            }
            function ja() {
                ja.b2DistanceJoint.apply(this, arguments);
                this.constructor === ja && this.b2DistanceJoint.apply(this,
                arguments)
            }
            function ka() {
                ka.b2DistanceJointDef.apply(this, arguments);
                this.constructor === ka && this.b2DistanceJointDef.apply(this, arguments)
            }
            function la() {
                la.b2FrictionJoint.apply(this, arguments);
                this.constructor === la && this.b2FrictionJoint.apply(this, arguments)
            }
            function ma() {
                ma.b2FrictionJointDef.apply(this, arguments);
                this.constructor === ma && this.b2FrictionJointDef.apply(this, arguments)
            }
            function na() {
                na.b2GearJoint.apply(this, arguments);
                this.constructor === na && this.b2GearJoint.apply(this, arguments)
            }
            function oa() {
                oa.b2GearJointDef.apply(this,
                arguments);
                this.constructor === oa && this.b2GearJointDef.apply(this, arguments)
            }
            function ab() {
                ab.b2Jacobian.apply(this, arguments)
            }
            function pa() {
                pa.b2Joint.apply(this, arguments);
                this.constructor === pa && this.b2Joint.apply(this, arguments)
            }
            function qa() {
                qa.b2JointDef.apply(this, arguments);
                this.constructor === qa && this.b2JointDef.apply(this, arguments)
            }
            function bb() {
                bb.b2JointEdge.apply(this, arguments)
            }
            function ra() {
                ra.b2LineJoint.apply(this, arguments);
                this.constructor === ra && this.b2LineJoint.apply(this, arguments)
            }

            function sa() {
                sa.b2LineJointDef.apply(this, arguments);
                this.constructor === sa && this.b2LineJointDef.apply(this, arguments)
            }
            function ta() {
                ta.b2MouseJoint.apply(this, arguments);
                this.constructor === ta && this.b2MouseJoint.apply(this, arguments)
            }
            function ua() {
                ua.b2MouseJointDef.apply(this, arguments);
                this.constructor === ua && this.b2MouseJointDef.apply(this, arguments)
            }
            function va() {
                va.b2PrismaticJoint.apply(this, arguments);
                this.constructor === va && this.b2PrismaticJoint.apply(this, arguments)
            }
            function wa() {
                wa.b2PrismaticJointDef.apply(this,
                arguments);
                this.constructor === wa && this.b2PrismaticJointDef.apply(this, arguments)
            }
            function xa() {
                xa.b2PulleyJoint.apply(this, arguments);
                this.constructor === xa && this.b2PulleyJoint.apply(this, arguments)
            }
            function ya() {
                ya.b2PulleyJointDef.apply(this, arguments);
                this.constructor === ya && this.b2PulleyJointDef.apply(this, arguments)
            }
            function za() {
                za.b2RevoluteJoint.apply(this, arguments);
                this.constructor === za && this.b2RevoluteJoint.apply(this, arguments)
            }
            function Aa() {
                Aa.b2RevoluteJointDef.apply(this, arguments);
                this.constructor === Aa && this.b2RevoluteJointDef.apply(this, arguments)
            }
            function Ba() {
                Ba.b2WeldJoint.apply(this, arguments);
                this.constructor === Ba && this.b2WeldJoint.apply(this, arguments)
            }
            function Ca() {
                Ca.b2WeldJointDef.apply(this, arguments);
                this.constructor === Ca && this.b2WeldJointDef.apply(this, arguments)
            }
            a.Collision.IBroadPhase = "Box2D.Collision.IBroadPhase";
            a.Collision.b2AABB = b;
            a.Collision.b2Bound = c;
            a.Collision.b2BoundValues = g;
            a.Collision.b2Collision = p;
            a.Collision.b2ContactID = f;
            a.Collision.b2ContactPoint = l;
            a.Collision.b2Distance = m;
            a.Collision.b2DistanceInput = d;
            a.Collision.b2DistanceOutput = t;
            a.Collision.b2DistanceProxy = r;
            a.Collision.b2DynamicTree = i;
            a.Collision.b2DynamicTreeBroadPhase = s;
            a.Collision.b2DynamicTreeNode = I;
            a.Collision.b2DynamicTreePair = w;
            a.Collision.b2Manifold = Q;
            a.Collision.b2ManifoldPoint = y;
            a.Collision.b2Point = o;
            a.Collision.b2RayCastInput = M;
            a.Collision.b2RayCastOutput = D;
            a.Collision.b2Segment = N;
            a.Collision.b2SeparationFunction = J;
            a.Collision.b2Simplex = K;
            a.Collision.b2SimplexCache = C;
            a.Collision.b2SimplexVertex = O;
            a.Collision.b2TimeOfImpact = E;
            a.Collision.b2TOIInput = H;
            a.Collision.b2WorldManifold = P;
            a.Collision.ClipVertex = L;
            a.Collision.Features = j;
            a.Collision.Shapes.b2CircleShape = n;
            a.Collision.Shapes.b2EdgeChainDef = q;
            a.Collision.Shapes.b2EdgeShape = h;
            a.Collision.Shapes.b2MassData = u;
            a.Collision.Shapes.b2PolygonShape = G;
            a.Collision.Shapes.b2Shape = x;
            a.Common.b2internal = "Box2D.Common.b2internal";
            a.Common.b2Color = e;
            a.Common.b2Settings = k;
            a.Common.Math.b2Mat22 = v;
            a.Common.Math.b2Mat33 = S;
            a.Common.Math.b2Math = Da;
            a.Common.Math.b2Sweep = Ea;
            a.Common.Math.b2Transform = T;
            a.Common.Math.b2Vec2 = U;
            a.Common.Math.b2Vec3 = V;
            a.Dynamics.b2Body = W;
            a.Dynamics.b2BodyDef = X;
            a.Dynamics.b2ContactFilter = Fa;
            a.Dynamics.b2ContactImpulse = Ga;
            a.Dynamics.b2ContactListener = Ha;
            a.Dynamics.b2ContactManager = Y;
            a.Dynamics.b2DebugDraw = Z;
            a.Dynamics.b2DestructionListener = Ia;
            a.Dynamics.b2FilterData = Ja;
            a.Dynamics.b2Fixture = $;
            a.Dynamics.b2FixtureDef = aa;
            a.Dynamics.b2Island = ba;
            a.Dynamics.b2TimeStep = Ka;
            a.Dynamics.b2World = ca;
            a.Dynamics.Contacts.b2CircleContact = La;
            a.Dynamics.Contacts.b2Contact = da;
            a.Dynamics.Contacts.b2ContactConstraint = ea;
            a.Dynamics.Contacts.b2ContactConstraintPoint = Ma;
            a.Dynamics.Contacts.b2ContactEdge = Na;
            a.Dynamics.Contacts.b2ContactFactory = fa;
            a.Dynamics.Contacts.b2ContactRegister = Oa;
            a.Dynamics.Contacts.b2ContactResult = Pa;
            a.Dynamics.Contacts.b2ContactSolver = ga;
            a.Dynamics.Contacts.b2EdgeAndCircleContact = Qa;
            a.Dynamics.Contacts.b2NullContact = ha;
            a.Dynamics.Contacts.b2PolyAndCircleContact = Ra;
            a.Dynamics.Contacts.b2PolyAndEdgeContact = Sa;
            a.Dynamics.Contacts.b2PolygonContact = Ta;
            a.Dynamics.Contacts.b2PositionSolverManifold = ia;
            a.Dynamics.Controllers.b2BuoyancyController = Ua;
            a.Dynamics.Controllers.b2ConstantAccelController = Va;
            a.Dynamics.Controllers.b2ConstantForceController = Wa;
            a.Dynamics.Controllers.b2Controller = Xa;
            a.Dynamics.Controllers.b2ControllerEdge = Ya;
            a.Dynamics.Controllers.b2GravityController = Za;
            a.Dynamics.Controllers.b2TensorDampingController = $a;
            a.Dynamics.Joints.b2DistanceJoint = ja;
            a.Dynamics.Joints.b2DistanceJointDef = ka;
            a.Dynamics.Joints.b2FrictionJoint = la;
            a.Dynamics.Joints.b2FrictionJointDef = ma;
            a.Dynamics.Joints.b2GearJoint = na;
            a.Dynamics.Joints.b2GearJointDef = oa;
            a.Dynamics.Joints.b2Jacobian = ab;
            a.Dynamics.Joints.b2Joint = pa;
            a.Dynamics.Joints.b2JointDef = qa;
            a.Dynamics.Joints.b2JointEdge = bb;
            a.Dynamics.Joints.b2LineJoint = ra;
            a.Dynamics.Joints.b2LineJointDef = sa;
            a.Dynamics.Joints.b2MouseJoint = ta;
            a.Dynamics.Joints.b2MouseJointDef = ua;
            a.Dynamics.Joints.b2PrismaticJoint = va;
            a.Dynamics.Joints.b2PrismaticJointDef = wa;
            a.Dynamics.Joints.b2PulleyJoint = xa;
            a.Dynamics.Joints.b2PulleyJointDef = ya;
            a.Dynamics.Joints.b2RevoluteJoint = za;
            a.Dynamics.Joints.b2RevoluteJointDef = Aa;
            a.Dynamics.Joints.b2WeldJoint = Ba;
            a.Dynamics.Joints.b2WeldJointDef = Ca
        })();
        a.postDefs = [];
        (function () {
            var b = a.Collision.Shapes.b2CircleShape,
                c = a.Collision.Shapes.b2PolygonShape,
                g = a.Collision.Shapes.b2Shape,
                p = a.Common.b2Settings,
                f = a.Common.Math.b2Math,
                l = a.Common.Math.b2Sweep,
                m = a.Common.Math.b2Transform,
                d = a.Common.Math.b2Vec2,
                t = a.Collision.b2AABB,
                i = a.Collision.b2Bound,
                B = a.Collision.b2BoundValues,
                F = a.Collision.b2Collision,
                I = a.Collision.b2ContactID,
                w = a.Collision.b2ContactPoint,
                Q = a.Collision.b2Distance,
                y = a.Collision.b2DistanceInput,
                o = a.Collision.b2DistanceOutput,
                M = a.Collision.b2DistanceProxy,
                D = a.Collision.b2DynamicTree,
                N = a.Collision.b2DynamicTreeBroadPhase,
                J = a.Collision.b2DynamicTreeNode,
                K = a.Collision.b2DynamicTreePair,
                C = a.Collision.b2Manifold,
                O = a.Collision.b2ManifoldPoint,
                E = a.Collision.b2Point,
                H = a.Collision.b2RayCastInput,
                P = a.Collision.b2RayCastOutput,
                L = a.Collision.b2Segment,
                j = a.Collision.b2SeparationFunction,
                n = a.Collision.b2Simplex,
                q = a.Collision.b2SimplexCache,
                h = a.Collision.b2SimplexVertex,
                u = a.Collision.b2TimeOfImpact,
                G = a.Collision.b2TOIInput,
                x = a.Collision.b2WorldManifold,
                e = a.Collision.ClipVertex,
                k = a.Collision.Features,
                v = a.Collision.IBroadPhase;
            t.b2AABB = function () {
                this.lowerBound = new d;
                this.upperBound = new d
            };
            t.prototype.IsValid = function () {
                var e = this.upperBound.y - this.lowerBound.y;
                return e = (e = this.upperBound.x - this.lowerBound.x >= 0 && e >= 0) && this.lowerBound.IsValid() && this.upperBound.IsValid()
            };
            t.prototype.GetCenter = function () {
                return new d((this.lowerBound.x + this.upperBound.x) / 2, (this.lowerBound.y + this.upperBound.y) / 2)
            };
            t.prototype.GetExtents = function () {
                return new d((this.upperBound.x - this.lowerBound.x) / 2, (this.upperBound.y - this.lowerBound.y) / 2)
            };
            t.prototype.Contains = function (e) {
                var a;
                return a = (a = (a = (a = this.lowerBound.x <= e.lowerBound.x) && this.lowerBound.y <= e.lowerBound.y) && e.upperBound.x <= this.upperBound.x) && e.upperBound.y <= this.upperBound.y
            };
            t.prototype.RayCast = function (e, a) {
                var k = -Number.MAX_VALUE,
                    j = Number.MAX_VALUE,
                    v = a.p1.x,
                    n = a.p1.y,
                    h = a.p2.x - a.p1.x,
                    b = a.p2.y - a.p1.y,
                    o = Math.abs(b),
                    d = e.normal,
                    q = 0,
                    x = 0,
                    u = q = 0,
                    u = 0;
                if (Math.abs(h) < Number.MIN_VALUE) {
                    if (v < this.lowerBound.x || this.upperBound.x < v) return !1
                } else {
                    q = 1 / h;
                    x = (this.lowerBound.x - v) * q;
                    q *= this.upperBound.x - v;
                    u = -1;
                    x > q && (u = x, x = q, q = u, u = 1);
                    if (x > k) d.x = u, d.y = 0, k = x;
                    j = Math.min(j, q);
                    if (k > j) return !1
                }
                if (o < Number.MIN_VALUE) {
                    if (n < this.lowerBound.y || this.upperBound.y < n) return !1
                } else {
                    q = 1 / b;
                    x = (this.lowerBound.y - n) * q;
                    q *= this.upperBound.y - n;
                    u = -1;
                    x > q && (u = x, x = q, q = u, u = 1);
                    if (x > k) d.y = u, d.x = 0, k = x;
                    j = Math.min(j, q);
                    if (k > j) return !1
                }
                e.fraction = k;
                return !0
            };
            t.prototype.TestOverlap = function (e) {
                var a = e.lowerBound.y - this.upperBound.y,
                    k = this.lowerBound.y - e.upperBound.y;
                if (e.lowerBound.x - this.upperBound.x > 0 || a > 0) return !1;
                if (this.lowerBound.x - e.upperBound.x > 0 || k > 0) return !1;
                return !0
            };
            t.Combine = function (e, a) {
                var k = new t;
                k.Combine(e, a);
                return k
            };
            t.prototype.Combine = function (e, a) {
                this.lowerBound.x = Math.min(e.lowerBound.x, a.lowerBound.x);
                this.lowerBound.y = Math.min(e.lowerBound.y, a.lowerBound.y);
                this.upperBound.x = Math.max(e.upperBound.x, a.upperBound.x);
                this.upperBound.y = Math.max(e.upperBound.y,
                a.upperBound.y)
            };
            i.b2Bound = function () {};
            i.prototype.IsLower = function () {
                return (this.value & 1) == 0
            };
            i.prototype.IsUpper = function () {
                return (this.value & 1) == 1
            };
            i.prototype.Swap = function (e) {
                var a = this.value,
                    k = this.proxy,
                    j = this.stabbingCount;
                this.value = e.value;
                this.proxy = e.proxy;
                this.stabbingCount = e.stabbingCount;
                e.value = a;
                e.proxy = k;
                e.stabbingCount = j
            };
            B.b2BoundValues = function () {};
            B.prototype.b2BoundValues = function () {
                this.lowerValues = new r;
                this.lowerValues[0] = 0;
                this.lowerValues[1] = 0;
                this.upperValues = new r;
                this.upperValues[0] = 0;
                this.upperValues[1] = 0
            };
            F.b2Collision = function () {};
            F.ClipSegmentToLine = function (e, a, k, j) {
                j === void 0 && (j = 0);
                var v, n = 0;
                v = a[0];
                var h = v.v;
                v = a[1];
                var q = v.v,
                    b = k.x * h.x + k.y * h.y - j;
                v = k.x * q.x + k.y * q.y - j;
                b <= 0 && e[n++].Set(a[0]);
                v <= 0 && e[n++].Set(a[1]);
                if (b * v < 0) k = b / (b - v), v = e[n], v = v.v, v.x = h.x + k * (q.x - h.x), v.y = h.y + k * (q.y - h.y), v = e[n], v.id = (b > 0 ? a[0] : a[1]).id, ++n;
                return n
            };
            F.EdgeSeparation = function (e, a, k, j, v) {
                k === void 0 && (k = 0);
                parseInt(e.m_vertexCount);
                var n = e.m_vertices,
                    e = e.m_normals,
                    h = parseInt(j.m_vertexCount),
                    q = j.m_vertices,
                    b, d;
                b = a.R;
                d = e[k];
                e = b.col1.x * d.x + b.col2.x * d.y;
                j = b.col1.y * d.x + b.col2.y * d.y;
                b = v.R;
                var o = b.col1.x * e + b.col1.y * j;
                b = b.col2.x * e + b.col2.y * j;
                for (var x = 0, u = Number.MAX_VALUE, c = 0; c < h; ++c) d = q[c], d = d.x * o + d.y * b, d < u && (u = d, x = c);
                d = n[k];
                b = a.R;
                k = a.position.x + (b.col1.x * d.x + b.col2.x * d.y);
                a = a.position.y + (b.col1.y * d.x + b.col2.y * d.y);
                d = q[x];
                b = v.R;
                n = v.position.x + (b.col1.x * d.x + b.col2.x * d.y);
                v = v.position.y + (b.col1.y * d.x + b.col2.y * d.y);
                n -= k;
                v -= a;
                return n * e + v * j
            };
            F.FindMaxSeparation = function (e, a, k, j, v) {
                var n = parseInt(a.m_vertexCount),
                    h = a.m_normals,
                    b, d;
                d = v.R;
                b = j.m_centroid;
                var q = v.position.x + (d.col1.x * b.x + d.col2.x * b.y),
                    o = v.position.y + (d.col1.y * b.x + d.col2.y * b.y);
                d = k.R;
                b = a.m_centroid;
                q -= k.position.x + (d.col1.x * b.x + d.col2.x * b.y);
                o -= k.position.y + (d.col1.y * b.x + d.col2.y * b.y);
                d = q * k.R.col1.x + o * k.R.col1.y;
                for (var o = q * k.R.col2.x + o * k.R.col2.y, q = 0, x = -Number.MAX_VALUE, u = 0; u < n; ++u) b = h[u], b = b.x * d + b.y * o, b > x && (x = b, q = u);
                h = F.EdgeSeparation(a, k, q, j, v);
                b = parseInt(q - 1 >= 0 ? q - 1 : n - 1);
                d = F.EdgeSeparation(a, k, b, j, v);
                var o = parseInt(q + 1 < n ? q + 1 : 0),
                    x = F.EdgeSeparation(a,
                    k, o, j, v),
                    c = u = 0,
                    G = 0;
                if (d > h && d > x) G = -1, u = b, c = d;
                else if (x > h) G = 1, u = o, c = x;
                else return e[0] = q, h;
                for (;;) if (q = G == -1 ? u - 1 >= 0 ? u - 1 : n - 1 : u + 1 < n ? u + 1 : 0, h = F.EdgeSeparation(a, k, q, j, v), h > c) u = q, c = h;
                else break;
                e[0] = u;
                return c
            };
            F.FindIncidentEdge = function (e, a, k, j, v, n) {
                j === void 0 && (j = 0);
                parseInt(a.m_vertexCount);
                var h = a.m_normals,
                    b = parseInt(v.m_vertexCount),
                    a = v.m_vertices,
                    v = v.m_normals,
                    d;
                d = k.R;
                var k = h[j],
                    h = d.col1.x * k.x + d.col2.x * k.y,
                    q = d.col1.y * k.x + d.col2.y * k.y;
                d = n.R;
                k = d.col1.x * h + d.col1.y * q;
                q = d.col2.x * h + d.col2.y * q;
                h = k;
                d = 0;
                for (var o = Number.MAX_VALUE, x = 0; x < b; ++x) k = v[x], k = h * k.x + q * k.y, k < o && (o = k, d = x);
                v = parseInt(d);
                h = parseInt(v + 1 < b ? v + 1 : 0);
                b = e[0];
                k = a[v];
                d = n.R;
                b.v.x = n.position.x + (d.col1.x * k.x + d.col2.x * k.y);
                b.v.y = n.position.y + (d.col1.y * k.x + d.col2.y * k.y);
                b.id.features.referenceEdge = j;
                b.id.features.incidentEdge = v;
                b.id.features.incidentVertex = 0;
                b = e[1];
                k = a[h];
                d = n.R;
                b.v.x = n.position.x + (d.col1.x * k.x + d.col2.x * k.y);
                b.v.y = n.position.y + (d.col1.y * k.x + d.col2.y * k.y);
                b.id.features.referenceEdge = j;
                b.id.features.incidentEdge = h;
                b.id.features.incidentVertex = 1
            };
            F.MakeClipPointVector = function () {
                var S = new s(2);
                S[0] = new e;
                S[1] = new e;
                return S
            };
            F.CollidePolygons = function (e, a, k, j, v) {
                var n;
                e.m_pointCount = 0;
                var h = a.m_radius + j.m_radius;
                F.s_edgeAO[0] = 0;
                var b = F.FindMaxSeparation(F.s_edgeAO, a, k, j, v);
                n = F.s_edgeAO[0];
                if (!(b > h)) {
                    var d;
                    F.s_edgeBO[0] = 0;
                    var q = F.FindMaxSeparation(F.s_edgeBO, j, v, a, k);
                    d = F.s_edgeBO[0];
                    if (!(q > h)) {
                        var o = 0,
                            x = 0;
                        q > 0.98 * b + 0.0010 ? (b = j, j = a, a = v, o = d, e.m_type = C.e_faceB, x = 1) : (b = a, a = k, k = v, o = n, e.m_type = C.e_faceA, x = 0);
                        n = F.s_incidentEdge;
                        F.FindIncidentEdge(n,
                        b, a, o, j, k);
                        d = parseInt(b.m_vertexCount);
                        var v = b.m_vertices,
                            b = v[o],
                            u;
                        u = o + 1 < d ? v[parseInt(o + 1)] : v[0];
                        o = F.s_localTangent;
                        o.Set(u.x - b.x, u.y - b.y);
                        o.Normalize();
                        v = F.s_localNormal;
                        v.x = o.y;
                        v.y = -o.x;
                        j = F.s_planePoint;
                        j.Set(0.5 * (b.x + u.x), 0.5 * (b.y + u.y));
                        q = F.s_tangent;
                        d = a.R;
                        q.x = d.col1.x * o.x + d.col2.x * o.y;
                        q.y = d.col1.y * o.x + d.col2.y * o.y;
                        var c = F.s_tangent2;
                        c.x = -q.x;
                        c.y = -q.y;
                        o = F.s_normal;
                        o.x = q.y;
                        o.y = -q.x;
                        var G = F.s_v11,
                            f = F.s_v12;
                        G.x = a.position.x + (d.col1.x * b.x + d.col2.x * b.y);
                        G.y = a.position.y + (d.col1.y * b.x + d.col2.y * b.y);
                        f.x = a.position.x + (d.col1.x * u.x + d.col2.x * u.y);
                        f.y = a.position.y + (d.col1.y * u.x + d.col2.y * u.y);
                        a = o.x * G.x + o.y * G.y;
                        d = q.x * f.x + q.y * f.y + h;
                        u = F.s_clipPoints1;
                        b = F.s_clipPoints2;
                        f = 0;
                        f = F.ClipSegmentToLine(u, n, c, -q.x * G.x - q.y * G.y + h);
                        if (!(f < 2) && (f = F.ClipSegmentToLine(b, u, q, d), !(f < 2))) {
                            e.m_localPlaneNormal.SetV(v);
                            e.m_localPoint.SetV(j);
                            for (j = v = 0; j < p.b2_maxManifoldPoints; ++j) if (n = b[j], o.x * n.v.x + o.y * n.v.y - a <= h) q = e.m_points[v], d = k.R, c = n.v.x - k.position.x, G = n.v.y - k.position.y, q.m_localPoint.x = c * d.col1.x + G * d.col1.y, q.m_localPoint.y = c * d.col2.x + G * d.col2.y, q.m_id.Set(n.id), q.m_id.features.flip = x, ++v;
                            e.m_pointCount = v
                        }
                    }
                }
            };
            F.CollideCircles = function (e, a, k, j, v) {
                e.m_pointCount = 0;
                var n, h;
                n = k.R;
                h = a.m_p;
                var b = k.position.x + (n.col1.x * h.x + n.col2.x * h.y),
                    k = k.position.y + (n.col1.y * h.x + n.col2.y * h.y);
                n = v.R;
                h = j.m_p;
                b = v.position.x + (n.col1.x * h.x + n.col2.x * h.y) - b;
                v = v.position.y + (n.col1.y * h.x + n.col2.y * h.y) - k;
                n = a.m_radius + j.m_radius;
                if (!(b * b + v * v > n * n)) e.m_type = C.e_circles, e.m_localPoint.SetV(a.m_p), e.m_localPlaneNormal.SetZero(), e.m_pointCount = 1, e.m_points[0].m_localPoint.SetV(j.m_p),
                e.m_points[0].m_id.key = 0
            };
            F.CollidePolygonAndCircle = function (e, a, k, j, v) {
                var n = e.m_pointCount = 0,
                    h = 0,
                    b, d;
                d = v.R;
                b = j.m_p;
                var q = v.position.y + (d.col1.y * b.x + d.col2.y * b.y),
                    n = v.position.x + (d.col1.x * b.x + d.col2.x * b.y) - k.position.x,
                    h = q - k.position.y;
                d = k.R;
                k = n * d.col1.x + h * d.col1.y;
                d = n * d.col2.x + h * d.col2.y;
                for (var o = 0, q = -Number.MAX_VALUE, v = a.m_radius + j.m_radius, x = parseInt(a.m_vertexCount), u = a.m_vertices, a = a.m_normals, c = 0; c < x; ++c) {
                    b = u[c];
                    n = k - b.x;
                    h = d - b.y;
                    b = a[c];
                    n = b.x * n + b.y * h;
                    if (n > v) return;
                    n > q && (q = n, o = c)
                }
                n = parseInt(o);
                h = parseInt(n + 1 < x ? n + 1 : 0);
                b = u[n];
                u = u[h];
                if (q < Number.MIN_VALUE) e.m_pointCount = 1, e.m_type = C.e_faceA, e.m_localPlaneNormal.SetV(a[o]), e.m_localPoint.x = 0.5 * (b.x + u.x), e.m_localPoint.y = 0.5 * (b.y + u.y);
                else if (q = (k - u.x) * (b.x - u.x) + (d - u.y) * (b.y - u.y), (k - b.x) * (u.x - b.x) + (d - b.y) * (u.y - b.y) <= 0) {
                    if ((k - b.x) * (k - b.x) + (d - b.y) * (d - b.y) > v * v) return;
                    e.m_pointCount = 1;
                    e.m_type = C.e_faceA;
                    e.m_localPlaneNormal.x = k - b.x;
                    e.m_localPlaneNormal.y = d - b.y;
                    e.m_localPlaneNormal.Normalize();
                    e.m_localPoint.SetV(b)
                } else if (q <= 0) {
                    if ((k - u.x) * (k - u.x) + (d - u.y) * (d - u.y) > v * v) return;
                    e.m_pointCount = 1;
                    e.m_type = C.e_faceA;
                    e.m_localPlaneNormal.x = k - u.x;
                    e.m_localPlaneNormal.y = d - u.y;
                    e.m_localPlaneNormal.Normalize();
                    e.m_localPoint.SetV(u)
                } else {
                    o = 0.5 * (b.x + u.x);
                    b = 0.5 * (b.y + u.y);
                    q = (k - o) * a[n].x + (d - b) * a[n].y;
                    if (q > v) return;
                    e.m_pointCount = 1;
                    e.m_type = C.e_faceA;
                    e.m_localPlaneNormal.x = a[n].x;
                    e.m_localPlaneNormal.y = a[n].y;
                    e.m_localPlaneNormal.Normalize();
                    e.m_localPoint.Set(o, b)
                }
                e.m_points[0].m_localPoint.SetV(j.m_p);
                e.m_points[0].m_id.key = 0
            };
            F.TestOverlap = function (e, a) {
                var k = a.lowerBound,
                    j = e.upperBound,
                    v = k.x - j.x,
                    n = k.y - j.y,
                    k = e.lowerBound,
                    j = a.upperBound,
                    b = k.y - j.y;
                if (v > 0 || n > 0) return !1;
                if (k.x - j.x > 0 || b > 0) return !1;
                return !0
            };
            a.postDefs.push(function () {
                a.Collision.b2Collision.s_incidentEdge = F.MakeClipPointVector();
                a.Collision.b2Collision.s_clipPoints1 = F.MakeClipPointVector();
                a.Collision.b2Collision.s_clipPoints2 = F.MakeClipPointVector();
                a.Collision.b2Collision.s_edgeAO = new r(1);
                a.Collision.b2Collision.s_edgeBO = new r(1);
                a.Collision.b2Collision.s_localTangent = new d;
                a.Collision.b2Collision.s_localNormal = new d;
                a.Collision.b2Collision.s_planePoint = new d;
                a.Collision.b2Collision.s_normal = new d;
                a.Collision.b2Collision.s_tangent = new d;
                a.Collision.b2Collision.s_tangent2 = new d;
                a.Collision.b2Collision.s_v11 = new d;
                a.Collision.b2Collision.s_v12 = new d;
                a.Collision.b2Collision.b2CollidePolyTempVec = new d;
                a.Collision.b2Collision.b2_nullFeature = 255
            });
            I.b2ContactID = function () {
                this.features = new k
            };
            I.prototype.b2ContactID = function () {
                this.features._m_id = this
            };
            I.prototype.Set = function (e) {
                this.key = e._key
            };
            I.prototype.Copy = function () {
                var e = new I;
                e.key = this.key;
                return e
            };
            Object.defineProperty(I.prototype, "key", {
                enumerable: !1,
                configurable: !0,
                get: function () {
                    return this._key
                }
            });
            Object.defineProperty(I.prototype, "key", {
                enumerable: !1,
                configurable: !0,
                set: function (e) {
                    e === void 0 && (e = 0);
                    this._key = e;
                    this.features._referenceEdge = this._key & 255;
                    this.features._incidentEdge = (this._key & 65280) >> 8 & 255;
                    this.features._incidentVertex = (this._key & 16711680) >> 16 & 255;
                    this.features._flip = (this._key & 4278190080) >> 24 & 255
                }
            });
            w.b2ContactPoint = function () {
                this.position = new d;
                this.velocity = new d;
                this.normal = new d;
                this.id = new I
            };
            Q.b2Distance = function () {};
            Q.Distance = function (e, a, k) {
                ++Q.b2_gjkCalls;
                var j = k.proxyA,
                    v = k.proxyB,
                    n = k.transformA,
                    b = k.transformB,
                    h = Q.s_simplex;
                h.ReadCache(a, j, n, v, b);
                var q = h.m_vertices,
                    o = Q.s_saveA,
                    u = Q.s_saveB,
                    x = 0;
                h.GetClosestPoint().LengthSquared();
                for (var c = 0, G, g = 0; g < 20;) {
                    x = h.m_count;
                    for (c = 0; c < x; c++) o[c] = q[c].indexA, u[c] = q[c].indexB;
                    switch (h.m_count) {
                        case 1:
                            break;
                        case 2:
                            h.Solve2();
                            break;
                        case 3:
                            h.Solve3();
                            break;
                        default:
                            p.b2Assert(!1)
                    }
                    if (h.m_count == 3) break;
                    G = h.GetClosestPoint();
                    G.LengthSquared();
                    c = h.GetSearchDirection();
                    if (c.LengthSquared() < Number.MIN_VALUE * Number.MIN_VALUE) break;
                    G = q[h.m_count];
                    G.indexA = j.GetSupport(f.MulTMV(n.R, c.GetNegative()));
                    G.wA = f.MulX(n, j.GetVertex(G.indexA));
                    G.indexB = v.GetSupport(f.MulTMV(b.R, c));
                    G.wB = f.MulX(b, v.GetVertex(G.indexB));
                    G.w = f.SubtractVV(G.wB, G.wA);
                    ++g;
                    ++Q.b2_gjkIters;
                    for (var t = !1, c = 0; c < x; c++) if (G.indexA == o[c] && G.indexB == u[c]) {
                        t = !0;
                        break
                    }
                    if (t) break;
                    ++h.m_count
                }
                Q.b2_gjkMaxIters = f.Max(Q.b2_gjkMaxIters, g);
                h.GetWitnessPoints(e.pointA,
                e.pointB);
                e.distance = f.SubtractVV(e.pointA, e.pointB).Length();
                e.iterations = g;
                h.WriteCache(a);
                if (k.useRadii) a = j.m_radius, v = v.m_radius, e.distance > a + v && e.distance > Number.MIN_VALUE ? (e.distance -= a + v, k = f.SubtractVV(e.pointB, e.pointA), k.Normalize(), e.pointA.x += a * k.x, e.pointA.y += a * k.y, e.pointB.x -= v * k.x, e.pointB.y -= v * k.y) : (G = new d, G.x = 0.5 * (e.pointA.x + e.pointB.x), G.y = 0.5 * (e.pointA.y + e.pointB.y), e.pointA.x = e.pointB.x = G.x, e.pointA.y = e.pointB.y = G.y, e.distance = 0)
            };
            a.postDefs.push(function () {
                a.Collision.b2Distance.s_simplex = new n;
                a.Collision.b2Distance.s_saveA = new r(3);
                a.Collision.b2Distance.s_saveB = new r(3)
            });
            y.b2DistanceInput = function () {};
            o.b2DistanceOutput = function () {
                this.pointA = new d;
                this.pointB = new d
            };
            M.b2DistanceProxy = function () {};
            M.prototype.Set = function (e) {
                switch (e.GetType()) {
                    case g.e_circleShape:
                        e = e instanceof b ? e : null;
                        this.m_vertices = new s(1, !0);
                        this.m_vertices[0] = e.m_p;
                        this.m_count = 1;
                        this.m_radius = e.m_radius;
                        break;
                    case g.e_polygonShape:
                        e = e instanceof c ? e : null;
                        this.m_vertices = e.m_vertices;
                        this.m_count = e.m_vertexCount;
                        this.m_radius = e.m_radius;
                        break;
                    default:
                        p.b2Assert(!1)
                }
            };
            M.prototype.GetSupport = function (e) {
                for (var a = 0, k = this.m_vertices[0].x * e.x + this.m_vertices[0].y * e.y, j = 1; j < this.m_count; ++j) {
                    var v = this.m_vertices[j].x * e.x + this.m_vertices[j].y * e.y;
                    v > k && (a = j, k = v)
                }
                return a
            };
            M.prototype.GetSupportVertex = function (e) {
                for (var a = 0, k = this.m_vertices[0].x * e.x + this.m_vertices[0].y * e.y, j = 1; j < this.m_count; ++j) {
                    var v = this.m_vertices[j].x * e.x + this.m_vertices[j].y * e.y;
                    v > k && (a = j, k = v)
                }
                return this.m_vertices[a]
            };
            M.prototype.GetVertexCount = function () {
                return this.m_count
            };
            M.prototype.GetVertex = function (e) {
                e === void 0 && (e = 0);
                p.b2Assert(0 <= e && e < this.m_count);
                return this.m_vertices[e]
            };
            D.b2DynamicTree = function () {};
            D.prototype.b2DynamicTree = function () {
                this.m_freeList = this.m_root = null;
                this.m_insertionCount = this.m_path = 0
            };
            D.prototype.CreateProxy = function (e, a) {
                var k = this.AllocateNode(),
                    j = p.b2_aabbExtension,
                    v = p.b2_aabbExtension;
                k.aabb.lowerBound.x = e.lowerBound.x - j;
                k.aabb.lowerBound.y = e.lowerBound.y - v;
                k.aabb.upperBound.x = e.upperBound.x + j;
                k.aabb.upperBound.y = e.upperBound.y + v;
                k.userData = a;
                this.InsertLeaf(k);
                return k
            };
            D.prototype.DestroyProxy = function (e) {
                this.RemoveLeaf(e);
                this.FreeNode(e)
            };
            D.prototype.MoveProxy = function (e, a, k) {
                p.b2Assert(e.IsLeaf());
                if (e.aabb.Contains(a)) return !1;
                this.RemoveLeaf(e);
                var j = p.b2_aabbExtension + p.b2_aabbMultiplier * (k.x > 0 ? k.x : -k.x),
                    k = p.b2_aabbExtension + p.b2_aabbMultiplier * (k.y > 0 ? k.y : -k.y);
                e.aabb.lowerBound.x = a.lowerBound.x - j;
                e.aabb.lowerBound.y = a.lowerBound.y - k;
                e.aabb.upperBound.x = a.upperBound.x + j;
                e.aabb.upperBound.y = a.upperBound.y + k;
                this.InsertLeaf(e);
                return !0
            };
            D.prototype.Rebalance = function (e) {
                e === void 0 && (e = 0);
                if (this.m_root != null) for (var a = 0; a < e; a++) {
                    for (var k = this.m_root, j = 0; k.IsLeaf() == !1;) k = this.m_path >> j & 1 ? k.child2 : k.child1, j = j + 1 & 31;
                    ++this.m_path;
                    this.RemoveLeaf(k);
                    this.InsertLeaf(k)
                }
            };
            D.prototype.GetFatAABB = function (e) {
                return e.aabb
            };
            D.prototype.GetUserData = function (e) {
                return e.userData
            };
            D.prototype.Query = function (e, a) {
                if (this.m_root != null) {
                    var k = new s,
                        j = 0;
                    for (k[j++] = this.m_root; j > 0;) {
                        var v = k[--j];
                        if (v.aabb.TestOverlap(a)) if (v.IsLeaf()) {
                            if (!e(v)) break
                        } else k[j++] = v.child1, k[j++] = v.child2
                    }
                }
            };
            D.prototype.RayCast = function (e, a) {
                if (this.m_root != null) {
                    var k = a.p1,
                        j = a.p2,
                        v = f.SubtractVV(k, j);
                    v.Normalize();
                    var v = f.CrossFV(1, v),
                        n = f.AbsV(v),
                        b = a.maxFraction,
                        h = new t,
                        d = 0,
                        q = 0,
                        d = k.x + b * (j.x - k.x),
                        q = k.y + b * (j.y - k.y);
                    h.lowerBound.x = Math.min(k.x, d);
                    h.lowerBound.y = Math.min(k.y, q);
                    h.upperBound.x = Math.max(k.x, d);
                    h.upperBound.y = Math.max(k.y, q);
                    var o = new s,
                        u = 0;
                    for (o[u++] = this.m_root; u > 0;) if (b = o[--u], b.aabb.TestOverlap(h) != !1 && (d = b.aabb.GetCenter(), q = b.aabb.GetExtents(), !(Math.abs(v.x * (k.x - d.x) + v.y * (k.y - d.y)) - n.x * q.x - n.y * q.y > 0))) if (b.IsLeaf()) {
                        d = new H;
                        d.p1 = a.p1;
                        d.p2 = a.p2;
                        d.maxFraction = a.maxFraction;
                        b = e(d, b);
                        if (b == 0) break;
                        if (b > 0) d = k.x + b * (j.x - k.x), q = k.y + b * (j.y - k.y), h.lowerBound.x = Math.min(k.x, d), h.lowerBound.y = Math.min(k.y, q), h.upperBound.x = Math.max(k.x, d), h.upperBound.y = Math.max(k.y, q)
                    } else o[u++] = b.child1, o[u++] = b.child2
                }
            };
            D.prototype.AllocateNode = function () {
                if (this.m_freeList) {
                    var e = this.m_freeList;
                    this.m_freeList = e.parent;
                    e.parent = null;
                    e.child1 = null;
                    e.child2 = null;
                    return e
                }
                return new J
            };
            D.prototype.FreeNode = function (e) {
                e.parent = this.m_freeList;
                this.m_freeList = e
            };
            D.prototype.InsertLeaf = function (e) {
                ++this.m_insertionCount;
                if (this.m_root == null) this.m_root = e, this.m_root.parent = null;
                else {
                    var k = e.aabb.GetCenter(),
                        a = this.m_root;
                    if (a.IsLeaf() == !1) {
                        do var j = a.child1,
                            a = a.child2,
                            a = Math.abs((j.aabb.lowerBound.x + j.aabb.upperBound.x) / 2 - k.x) + Math.abs((j.aabb.lowerBound.y + j.aabb.upperBound.y) / 2 - k.y) < Math.abs((a.aabb.lowerBound.x + a.aabb.upperBound.x) / 2 - k.x) + Math.abs((a.aabb.lowerBound.y + a.aabb.upperBound.y) / 2 - k.y) ? j : a;
                        while (a.IsLeaf() == !1)
                    }
                    k = a.parent;
                    j = this.AllocateNode();
                    j.parent = k;
                    j.userData = null;
                    j.aabb.Combine(e.aabb, a.aabb);
                    if (k) {
                        a.parent.child1 == a ? k.child1 = j : k.child2 = j;
                        j.child1 = a;
                        j.child2 = e;
                        a.parent = j;
                        e.parent = j;
                        do {
                            if (k.aabb.Contains(j.aabb)) break;
                            k.aabb.Combine(k.child1.aabb, k.child2.aabb);
                            j = k;
                            k = k.parent
                        } while (k)
                    } else j.child1 = a, j.child2 = e, a.parent = j, this.m_root = e.parent = j
                }
            };
            D.prototype.RemoveLeaf = function (e) {
                if (e == this.m_root) this.m_root = null;
                else {
                    var k = e.parent,
                        a = k.parent,
                        e = k.child1 == e ? k.child2 : k.child1;
                    if (a) {
                        a.child1 == k ? a.child1 = e : a.child2 = e;
                        e.parent = a;
                        for (this.FreeNode(k); a;) {
                            k = a.aabb;
                            a.aabb = t.Combine(a.child1.aabb, a.child2.aabb);
                            if (k.Contains(a.aabb)) break;
                            a = a.parent
                        }
                    } else this.m_root = e, e.parent = null, this.FreeNode(k)
                }
            };
            N.b2DynamicTreeBroadPhase = function () {
                this.m_tree = new D;
                this.m_moveBuffer = new s;
                this.m_pairBuffer = new s;
                this.m_pairCount = 0
            };
            N.prototype.CreateProxy = function (e, k) {
                var a = this.m_tree.CreateProxy(e, k);
                ++this.m_proxyCount;
                this.BufferMove(a);
                return a
            };
            N.prototype.DestroyProxy = function (e) {
                this.UnBufferMove(e);
                --this.m_proxyCount;
                this.m_tree.DestroyProxy(e)
            };
            N.prototype.MoveProxy = function (e, k, a) {
                this.m_tree.MoveProxy(e, k, a) && this.BufferMove(e)
            };
            N.prototype.TestOverlap = function (e, k) {
                var a = this.m_tree.GetFatAABB(e),
                    j = this.m_tree.GetFatAABB(k);
                return a.TestOverlap(j)
            };
            N.prototype.GetUserData = function (e) {
                return this.m_tree.GetUserData(e)
            };
            N.prototype.GetFatAABB = function (e) {
                return this.m_tree.GetFatAABB(e)
            };
            N.prototype.GetProxyCount = function () {
                return this.m_proxyCount
            };
            N.prototype.UpdatePairs = function (e) {
                for (var k = this, a = k.m_pairCount = 0, j, a = 0; a < k.m_moveBuffer.length; ++a) {
                    j = k.m_moveBuffer[a];
                    var v = k.m_tree.GetFatAABB(j);
                    k.m_tree.Query(function (e) {
                        if (e == j) return !0;
                        k.m_pairCount == k.m_pairBuffer.length && (k.m_pairBuffer[k.m_pairCount] = new K);
                        var a = k.m_pairBuffer[k.m_pairCount];
                        a.proxyA = e < j ? e : j;
                        a.proxyB = e >= j ? e : j;
                        ++k.m_pairCount;
                        return !0
                    }, v)
                }
                for (a = k.m_moveBuffer.length = 0; a < k.m_pairCount;) {
                    var v = k.m_pairBuffer[a],
                        b = k.m_tree.GetUserData(v.proxyA),
                        n = k.m_tree.GetUserData(v.proxyB);
                    e(b, n);
                    for (++a; a < k.m_pairCount;) {
                        b = k.m_pairBuffer[a];
                        if (b.proxyA != v.proxyA || b.proxyB != v.proxyB) break;
                        ++a
                    }
                }
            };
            N.prototype.Query = function (e, k) {
                this.m_tree.Query(e, k)
            };
            N.prototype.RayCast = function (e, k) {
                this.m_tree.RayCast(e, k)
            };
            N.prototype.Validate = function () {};
            N.prototype.Rebalance = function (e) {
                e === void 0 && (e = 0);
                this.m_tree.Rebalance(e)
            };
            N.prototype.BufferMove = function (e) {
                this.m_moveBuffer[this.m_moveBuffer.length] = e
            };
            N.prototype.UnBufferMove = function (e) {
                this.m_moveBuffer.splice(parseInt(this.m_moveBuffer.indexOf(e)),
                1)
            };
            N.prototype.ComparePairs = function () {
                return 0
            };
            N.__implements = {};
            N.__implements[v] = !0;
            J.b2DynamicTreeNode = function () {
                this.aabb = new t
            };
            J.prototype.IsLeaf = function () {
                return this.child1 == null
            };
            K.b2DynamicTreePair = function () {};
            C.b2Manifold = function () {
                this.m_pointCount = 0
            };
            C.prototype.b2Manifold = function () {
                this.m_points = new s(p.b2_maxManifoldPoints);
                for (var e = 0; e < p.b2_maxManifoldPoints; e++) this.m_points[e] = new O;
                this.m_localPlaneNormal = new d;
                this.m_localPoint = new d
            };
            C.prototype.Reset = function () {
                for (var e = 0; e < p.b2_maxManifoldPoints; e++)(this.m_points[e] instanceof O ? this.m_points[e] : null).Reset();
                this.m_localPlaneNormal.SetZero();
                this.m_localPoint.SetZero();
                this.m_pointCount = this.m_type = 0
            };
            C.prototype.Set = function (e) {
                this.m_pointCount = e.m_pointCount;
                for (var k = 0; k < p.b2_maxManifoldPoints; k++)(this.m_points[k] instanceof O ? this.m_points[k] : null).Set(e.m_points[k]);
                this.m_localPlaneNormal.SetV(e.m_localPlaneNormal);
                this.m_localPoint.SetV(e.m_localPoint);
                this.m_type = e.m_type
            };
            C.prototype.Copy = function () {
                var e = new C;
                e.Set(this);
                return e
            };
            a.postDefs.push(function () {
                a.Collision.b2Manifold.e_circles = 1;
                a.Collision.b2Manifold.e_faceA = 2;
                a.Collision.b2Manifold.e_faceB = 4
            });
            O.b2ManifoldPoint = function () {
                this.m_localPoint = new d;
                this.m_id = new I
            };
            O.prototype.b2ManifoldPoint = function () {
                this.Reset()
            };
            O.prototype.Reset = function () {
                this.m_localPoint.SetZero();
                this.m_tangentImpulse = this.m_normalImpulse = 0;
                this.m_id.key = 0
            };
            O.prototype.Set = function (e) {
                this.m_localPoint.SetV(e.m_localPoint);
                this.m_normalImpulse = e.m_normalImpulse;
                this.m_tangentImpulse = e.m_tangentImpulse;
                this.m_id.Set(e.m_id)
            };
            E.b2Point = function () {
                this.p = new d
            };
            E.prototype.Support = function () {
                return this.p
            };
            E.prototype.GetFirstVertex = function () {
                return this.p
            };
            H.b2RayCastInput = function () {
                this.p1 = new d;
                this.p2 = new d
            };
            H.prototype.b2RayCastInput = function (e, k, a) {
                e === void 0 && (e = null);
                k === void 0 && (k = null);
                a === void 0 && (a = 1);
                e && this.p1.SetV(e);
                k && this.p2.SetV(k);
                this.maxFraction = a
            };
            P.b2RayCastOutput = function () {
                this.normal = new d
            };
            L.b2Segment = function () {
                this.p1 = new d;
                this.p2 = new d
            };
            L.prototype.TestSegment = function (e, k, a, j) {
                j === void 0 && (j = 0);
                var v = a.p1,
                    b = a.p2.x - v.x,
                    n = a.p2.y - v.y,
                    a = this.p2.y - this.p1.y,
                    h = -(this.p2.x - this.p1.x),
                    d = 100 * Number.MIN_VALUE,
                    q = -(b * a + n * h);
                if (q > d) {
                    var o = v.x - this.p1.x,
                        u = v.y - this.p1.y,
                        v = o * a + u * h;
                    if (0 <= v && v <= j * q && (j = -b * u + n * o, -d * q <= j && j <= q * (1 + d))) return v /= q, j = Math.sqrt(a * a + h * h), a /= j, h /= j, e[0] = v, k.Set(a, h), !0
                }
                return !1
            };
            L.prototype.Extend = function (e) {
                this.ExtendForward(e);
                this.ExtendBackward(e)
            };
            L.prototype.ExtendForward = function (e) {
                var k = this.p2.x - this.p1.x,
                    a = this.p2.y - this.p1.y,
                    e = Math.min(k > 0 ? (e.upperBound.x - this.p1.x) / k : k < 0 ? (e.lowerBound.x - this.p1.x) / k : Number.POSITIVE_INFINITY, a > 0 ? (e.upperBound.y - this.p1.y) / a : a < 0 ? (e.lowerBound.y - this.p1.y) / a : Number.POSITIVE_INFINITY);
                this.p2.x = this.p1.x + k * e;
                this.p2.y = this.p1.y + a * e
            };
            L.prototype.ExtendBackward = function (e) {
                var k = -this.p2.x + this.p1.x,
                    a = -this.p2.y + this.p1.y,
                    e = Math.min(k > 0 ? (e.upperBound.x - this.p2.x) / k : k < 0 ? (e.lowerBound.x - this.p2.x) / k : Number.POSITIVE_INFINITY, a > 0 ? (e.upperBound.y - this.p2.y) / a : a < 0 ? (e.lowerBound.y - this.p2.y) / a : Number.POSITIVE_INFINITY);
                this.p1.x = this.p2.x + k * e;
                this.p1.y = this.p2.y + a * e
            };
            j.b2SeparationFunction = function () {
                this.m_localPoint = new d;
                this.m_axis = new d
            };
            j.prototype.Initialize = function (e, k, a, v, b) {
                this.m_proxyA = k;
                this.m_proxyB = v;
                var n = parseInt(e.count);
                p.b2Assert(0 < n && n < 3);
                var h, q, o, u, x = u = o = v = k = 0,
                    c = 0,
                    x = 0;
                n == 1 ? (this.m_type = j.e_points, h = this.m_proxyA.GetVertex(e.indexA[0]), q = this.m_proxyB.GetVertex(e.indexB[0]), n = h, e = a.R, k = a.position.x + (e.col1.x * n.x + e.col2.x * n.y), v = a.position.y + (e.col1.y * n.x + e.col2.y * n.y), n = q, e = b.R, o = b.position.x + (e.col1.x * n.x + e.col2.x * n.y), u = b.position.y + (e.col1.y * n.x + e.col2.y * n.y), this.m_axis.x = o - k, this.m_axis.y = u - v, this.m_axis.Normalize()) : (e.indexB[0] == e.indexB[1] ? (this.m_type = j.e_faceA, k = this.m_proxyA.GetVertex(e.indexA[0]), v = this.m_proxyA.GetVertex(e.indexA[1]), q = this.m_proxyB.GetVertex(e.indexB[0]), this.m_localPoint.x = 0.5 * (k.x + v.x), this.m_localPoint.y = 0.5 * (k.y + v.y), this.m_axis = f.CrossVF(f.SubtractVV(v, k), 1), this.m_axis.Normalize(), n = this.m_axis, e = a.R, x = e.col1.x * n.x + e.col2.x * n.y, c = e.col1.y * n.x + e.col2.y * n.y, n = this.m_localPoint, e = a.R, k = a.position.x + (e.col1.x * n.x + e.col2.x * n.y), v = a.position.y + (e.col1.y * n.x + e.col2.y * n.y), n = q, e = b.R, o = b.position.x + (e.col1.x * n.x + e.col2.x * n.y), u = b.position.y + (e.col1.y * n.x + e.col2.y * n.y), x = (o - k) * x + (u - v) * c) : e.indexA[0] == e.indexA[0] ? (this.m_type = j.e_faceB, o = this.m_proxyB.GetVertex(e.indexB[0]), u = this.m_proxyB.GetVertex(e.indexB[1]), h = this.m_proxyA.GetVertex(e.indexA[0]), this.m_localPoint.x = 0.5 * (o.x + u.x), this.m_localPoint.y = 0.5 * (o.y + u.y), this.m_axis = f.CrossVF(f.SubtractVV(u, o), 1), this.m_axis.Normalize(), n = this.m_axis, e = b.R, x = e.col1.x * n.x + e.col2.x * n.y, c = e.col1.y * n.x + e.col2.y * n.y, n = this.m_localPoint, e = b.R, o = b.position.x + (e.col1.x * n.x + e.col2.x * n.y), u = b.position.y + (e.col1.y * n.x + e.col2.y * n.y), n = h, e = a.R, k = a.position.x + (e.col1.x * n.x + e.col2.x * n.y), v = a.position.y + (e.col1.y * n.x + e.col2.y * n.y), x = (k - o) * x + (v - u) * c) : (k = this.m_proxyA.GetVertex(e.indexA[0]), v = this.m_proxyA.GetVertex(e.indexA[1]), o = this.m_proxyB.GetVertex(e.indexB[0]),
                u = this.m_proxyB.GetVertex(e.indexB[1]), f.MulX(a, h), h = f.MulMV(a.R, f.SubtractVV(v, k)), f.MulX(b, q), x = f.MulMV(b.R, f.SubtractVV(u, o)), b = h.x * h.x + h.y * h.y, q = x.x * x.x + x.y * x.y, e = f.SubtractVV(x, h), a = h.x * e.x + h.y * e.y, e = x.x * e.x + x.y * e.y, h = h.x * x.x + h.y * x.y, c = b * q - h * h, x = 0, c != 0 && (x = f.Clamp((h * e - a * q) / c, 0, 1)), (h * x + e) / q < 0 && (x = f.Clamp((h - a) / b, 0, 1)), h = new d, h.x = k.x + x * (v.x - k.x), h.y = k.y + x * (v.y - k.y), q = new d, q.x = o.x + x * (u.x - o.x), q.y = o.y + x * (u.y - o.y), x == 0 || x == 1 ? (this.m_type = j.e_faceB, this.m_axis = f.CrossVF(f.SubtractVV(u, o), 1), this.m_axis.Normalize(),
                this.m_localPoint = q) : (this.m_type = j.e_faceA, this.m_axis = f.CrossVF(f.SubtractVV(v, k), 1), this.m_localPoint = h)), x < 0 && this.m_axis.NegativeSelf())
            };
            j.prototype.Evaluate = function (e, k) {
                var a, v, n = 0;
                switch (this.m_type) {
                    case j.e_points:
                        return a = f.MulTMV(e.R, this.m_axis), v = f.MulTMV(k.R, this.m_axis.GetNegative()), a = this.m_proxyA.GetSupportVertex(a), v = this.m_proxyB.GetSupportVertex(v), a = f.MulX(e, a), v = f.MulX(k, v), n = (v.x - a.x) * this.m_axis.x + (v.y - a.y) * this.m_axis.y;
                    case j.e_faceA:
                        return n = f.MulMV(e.R, this.m_axis),
                        a = f.MulX(e, this.m_localPoint), v = f.MulTMV(k.R, n.GetNegative()), v = this.m_proxyB.GetSupportVertex(v), v = f.MulX(k, v), n = (v.x - a.x) * n.x + (v.y - a.y) * n.y;
                    case j.e_faceB:
                        return n = f.MulMV(k.R, this.m_axis), v = f.MulX(k, this.m_localPoint), a = f.MulTMV(e.R, n.GetNegative()), a = this.m_proxyA.GetSupportVertex(a), a = f.MulX(e, a), n = (a.x - v.x) * n.x + (a.y - v.y) * n.y;
                    default:
                        return p.b2Assert(!1), 0
                }
            };
            a.postDefs.push(function () {
                a.Collision.b2SeparationFunction.e_points = 1;
                a.Collision.b2SeparationFunction.e_faceA = 2;
                a.Collision.b2SeparationFunction.e_faceB = 4
            });
            n.b2Simplex = function () {
                this.m_v1 = new h;
                this.m_v2 = new h;
                this.m_v3 = new h;
                this.m_vertices = new s(3)
            };
            n.prototype.b2Simplex = function () {
                this.m_vertices[0] = this.m_v1;
                this.m_vertices[1] = this.m_v2;
                this.m_vertices[2] = this.m_v3
            };
            n.prototype.ReadCache = function (e, k, a, j, v) {
                p.b2Assert(0 <= e.count && e.count <= 3);
                var n, h;
                this.m_count = e.count;
                for (var b = this.m_vertices, d = 0; d < this.m_count; d++) {
                    var q = b[d];
                    q.indexA = e.indexA[d];
                    q.indexB = e.indexB[d];
                    n = k.GetVertex(q.indexA);
                    h = j.GetVertex(q.indexB);
                    q.wA = f.MulX(a, n);
                    q.wB = f.MulX(v, h);
                    q.w = f.SubtractVV(q.wB, q.wA);
                    q.a = 0
                }
                if (this.m_count > 1 && (e = e.metric, n = this.GetMetric(), n < 0.5 * e || 2 * e < n || n < Number.MIN_VALUE)) this.m_count = 0;
                if (this.m_count == 0) q = b[0], q.indexA = 0, q.indexB = 0, n = k.GetVertex(0), h = j.GetVertex(0), q.wA = f.MulX(a, n), q.wB = f.MulX(v, h), q.w = f.SubtractVV(q.wB, q.wA), this.m_count = 1
            };
            n.prototype.WriteCache = function (e) {
                e.metric = this.GetMetric();
                e.count = a.parseUInt(this.m_count);
                for (var k = this.m_vertices, j = 0; j < this.m_count; j++) e.indexA[j] = a.parseUInt(k[j].indexA), e.indexB[j] = a.parseUInt(k[j].indexB)
            };
            n.prototype.GetSearchDirection = function () {
                switch (this.m_count) {
                    case 1:
                        return this.m_v1.w.GetNegative();
                    case 2:
                        var e = f.SubtractVV(this.m_v2.w, this.m_v1.w);
                        return f.CrossVV(e, this.m_v1.w.GetNegative()) > 0 ? f.CrossFV(1, e) : f.CrossVF(e, 1);
                    default:
                        return p.b2Assert(!1), new d
                }
            };
            n.prototype.GetClosestPoint = function () {
                switch (this.m_count) {
                    case 0:
                        return p.b2Assert(!1), new d;
                    case 1:
                        return this.m_v1.w;
                    case 2:
                        return new d(this.m_v1.a * this.m_v1.w.x + this.m_v2.a * this.m_v2.w.x, this.m_v1.a * this.m_v1.w.y + this.m_v2.a * this.m_v2.w.y);
                    default:
                        return p.b2Assert(!1), new d
                }
            };
            n.prototype.GetWitnessPoints = function (e, k) {
                switch (this.m_count) {
                    case 0:
                        p.b2Assert(!1);
                        break;
                    case 1:
                        e.SetV(this.m_v1.wA);
                        k.SetV(this.m_v1.wB);
                        break;
                    case 2:
                        e.x = this.m_v1.a * this.m_v1.wA.x + this.m_v2.a * this.m_v2.wA.x;
                        e.y = this.m_v1.a * this.m_v1.wA.y + this.m_v2.a * this.m_v2.wA.y;
                        k.x = this.m_v1.a * this.m_v1.wB.x + this.m_v2.a * this.m_v2.wB.x;
                        k.y = this.m_v1.a * this.m_v1.wB.y + this.m_v2.a * this.m_v2.wB.y;
                        break;
                    case 3:
                        k.x = e.x = this.m_v1.a * this.m_v1.wA.x + this.m_v2.a * this.m_v2.wA.x + this.m_v3.a * this.m_v3.wA.x;
                        k.y = e.y = this.m_v1.a * this.m_v1.wA.y + this.m_v2.a * this.m_v2.wA.y + this.m_v3.a * this.m_v3.wA.y;
                        break;
                    default:
                        p.b2Assert(!1)
                }
            };
            n.prototype.GetMetric = function () {
                switch (this.m_count) {
                    case 0:
                        return p.b2Assert(!1), 0;
                    case 1:
                        return 0;
                    case 2:
                        return f.SubtractVV(this.m_v1.w, this.m_v2.w).Length();
                    case 3:
                        return f.CrossVV(f.SubtractVV(this.m_v2.w, this.m_v1.w), f.SubtractVV(this.m_v3.w, this.m_v1.w));
                    default:
                        return p.b2Assert(!1), 0
                }
            };
            n.prototype.Solve2 = function () {
                var e = this.m_v1.w,
                    k = this.m_v2.w,
                    a = f.SubtractVV(k, e),
                    e = -(e.x * a.x + e.y * a.y);
                e <= 0 ? this.m_count = this.m_v1.a = 1 : (k = k.x * a.x + k.y * a.y, k <= 0 ? (this.m_count = this.m_v2.a = 1, this.m_v1.Set(this.m_v2)) : (a = 1 / (k + e), this.m_v1.a = k * a, this.m_v2.a = e * a, this.m_count = 2))
            };
            n.prototype.Solve3 = function () {
                var e = this.m_v1.w,
                    k = this.m_v2.w,
                    a = this.m_v3.w,
                    j = f.SubtractVV(k, e),
                    v = f.Dot(e, j),
                    n = f.Dot(k, j),
                    v = -v,
                    h = f.SubtractVV(a, e),
                    b = f.Dot(e, h),
                    d = f.Dot(a, h),
                    b = -b,
                    q = f.SubtractVV(a, k),
                    o = f.Dot(k, q),
                    q = f.Dot(a, q),
                    o = -o,
                    h = f.CrossVV(j, h),
                    j = h * f.CrossVV(k, a),
                    a = h * f.CrossVV(a, e),
                    e = h * f.CrossVV(e,
                    k);
                v <= 0 && b <= 0 ? this.m_count = this.m_v1.a = 1 : n > 0 && v > 0 && e <= 0 ? (d = 1 / (n + v), this.m_v1.a = n * d, this.m_v2.a = v * d, this.m_count = 2) : d > 0 && b > 0 && a <= 0 ? (n = 1 / (d + b), this.m_v1.a = d * n, this.m_v3.a = b * n, this.m_count = 2, this.m_v2.Set(this.m_v3)) : n <= 0 && o <= 0 ? (this.m_count = this.m_v2.a = 1, this.m_v1.Set(this.m_v2)) : d <= 0 && q <= 0 ? (this.m_count = this.m_v3.a = 1, this.m_v1.Set(this.m_v3)) : q > 0 && o > 0 && j <= 0 ? (n = 1 / (q + o), this.m_v2.a = q * n, this.m_v3.a = o * n, this.m_count = 2, this.m_v1.Set(this.m_v3)) : (n = 1 / (j + a + e), this.m_v1.a = j * n, this.m_v2.a = a * n, this.m_v3.a = e * n, this.m_count = 3)
            };
            q.b2SimplexCache = function () {
                this.indexA = new r(3);
                this.indexB = new r(3)
            };
            h.b2SimplexVertex = function () {};
            h.prototype.Set = function (e) {
                this.wA.SetV(e.wA);
                this.wB.SetV(e.wB);
                this.w.SetV(e.w);
                this.a = e.a;
                this.indexA = e.indexA;
                this.indexB = e.indexB
            };
            u.b2TimeOfImpact = function () {};
            u.TimeOfImpact = function (e) {
                ++u.b2_toiCalls;
                var k = e.proxyA,
                    a = e.proxyB,
                    j = e.sweepA,
                    v = e.sweepB;
                p.b2Assert(j.t0 == v.t0);
                p.b2Assert(1 - j.t0 > Number.MIN_VALUE);
                var n = k.m_radius + a.m_radius,
                    e = e.tolerance,
                    h = 0,
                    b = 0,
                    d = 0;
                u.s_cache.count = 0;
                for (u.s_distanceInput.useRadii = !1;;) {
                    j.GetTransform(u.s_xfA, h);
                    v.GetTransform(u.s_xfB, h);
                    u.s_distanceInput.proxyA = k;
                    u.s_distanceInput.proxyB = a;
                    u.s_distanceInput.transformA = u.s_xfA;
                    u.s_distanceInput.transformB = u.s_xfB;
                    Q.Distance(u.s_distanceOutput, u.s_cache, u.s_distanceInput);
                    if (u.s_distanceOutput.distance <= 0) {
                        h = 1;
                        break
                    }
                    u.s_fcn.Initialize(u.s_cache, k, u.s_xfA, a, u.s_xfB);
                    var q = u.s_fcn.Evaluate(u.s_xfA, u.s_xfB);
                    if (q <= 0) {
                        h = 1;
                        break
                    }
                    b == 0 && (d = q > n ? f.Max(n - e, 0.75 * n) : f.Max(q - e, 0.02 * n));
                    if (q - d < 0.5 * e) {
                        if (b == 0) {
                            h = 1;
                            break
                        }
                        break
                    }
                    var o = h,
                        x = h,
                        c = 1;
                    j.GetTransform(u.s_xfA, c);
                    v.GetTransform(u.s_xfB, c);
                    var G = u.s_fcn.Evaluate(u.s_xfA, u.s_xfB);
                    if (G >= d) {
                        h = 1;
                        break
                    }
                    for (var g = 0;;) {
                        var t = 0,
                            t = g & 1 ? x + (d - q) * (c - x) / (G - q) : 0.5 * (x + c);
                        j.GetTransform(u.s_xfA, t);
                        v.GetTransform(u.s_xfB, t);
                        var l = u.s_fcn.Evaluate(u.s_xfA, u.s_xfB);
                        if (f.Abs(l - d) < 0.025 * e) {
                            o = t;
                            break
                        }
                        l > d ? (x = t, q = l) : (c = t, G = l);
                        ++g;
                        ++u.b2_toiRootIters;
                        if (g == 50) break
                    }
                    u.b2_toiMaxRootIters = f.Max(u.b2_toiMaxRootIters, g);
                    if (o < (1 + 100 * Number.MIN_VALUE) * h) break;
                    h = o;
                    b++;
                    ++u.b2_toiIters;
                    if (b == 1E3) break
                }
                u.b2_toiMaxIters = f.Max(u.b2_toiMaxIters, b);
                return h
            };
            a.postDefs.push(function () {
                a.Collision.b2TimeOfImpact.b2_toiCalls = 0;
                a.Collision.b2TimeOfImpact.b2_toiIters = 0;
                a.Collision.b2TimeOfImpact.b2_toiMaxIters = 0;
                a.Collision.b2TimeOfImpact.b2_toiRootIters = 0;
                a.Collision.b2TimeOfImpact.b2_toiMaxRootIters = 0;
                a.Collision.b2TimeOfImpact.s_cache = new q;
                a.Collision.b2TimeOfImpact.s_distanceInput = new y;
                a.Collision.b2TimeOfImpact.s_xfA = new m;
                a.Collision.b2TimeOfImpact.s_xfB = new m;
                a.Collision.b2TimeOfImpact.s_fcn = new j;
                a.Collision.b2TimeOfImpact.s_distanceOutput = new o
            });
            G.b2TOIInput = function () {
                this.proxyA = new M;
                this.proxyB = new M;
                this.sweepA = new l;
                this.sweepB = new l
            };
            x.b2WorldManifold = function () {
                this.m_normal = new d
            };
            x.prototype.b2WorldManifold = function () {
                this.m_points = new s(p.b2_maxManifoldPoints);
                for (var e = 0; e < p.b2_maxManifoldPoints; e++) this.m_points[e] = new d
            };
            x.prototype.Initialize = function (e, k, a, j, v) {
                a === void 0 && (a = 0);
                v === void 0 && (v = 0);
                if (e.m_pointCount != 0) {
                    var n = 0,
                        h, b, d = 0,
                        q = 0,
                        o = 0,
                        u = 0,
                        x = 0;
                    h = 0;
                    switch (e.m_type) {
                        case C.e_circles:
                            b = k.R;
                            h = e.m_localPoint;
                            n = k.position.x + b.col1.x * h.x + b.col2.x * h.y;
                            k = k.position.y + b.col1.y * h.x + b.col2.y * h.y;
                            b = j.R;
                            h = e.m_points[0].m_localPoint;
                            e = j.position.x + b.col1.x * h.x + b.col2.x * h.y;
                            j = j.position.y + b.col1.y * h.x + b.col2.y * h.y;
                            h = e - n;
                            b = j - k;
                            d = h * h + b * b;
                            d > Number.MIN_VALUE * Number.MIN_VALUE ? (d = Math.sqrt(d), this.m_normal.x = h / d, this.m_normal.y = b / d) : (this.m_normal.x = 1, this.m_normal.y = 0);
                            h = k + a * this.m_normal.y;
                            j -= v * this.m_normal.y;
                            this.m_points[0].x = 0.5 * (n + a * this.m_normal.x + (e - v * this.m_normal.x));
                            this.m_points[0].y = 0.5 * (h + j);
                            break;
                        case C.e_faceA:
                            b = k.R;
                            h = e.m_localPlaneNormal;
                            d = b.col1.x * h.x + b.col2.x * h.y;
                            q = b.col1.y * h.x + b.col2.y * h.y;
                            b = k.R;
                            h = e.m_localPoint;
                            o = k.position.x + b.col1.x * h.x + b.col2.x * h.y;
                            u = k.position.y + b.col1.y * h.x + b.col2.y * h.y;
                            this.m_normal.x = d;
                            this.m_normal.y = q;
                            for (n = 0; n < e.m_pointCount; n++) b = j.R, h = e.m_points[n].m_localPoint, x = j.position.x + b.col1.x * h.x + b.col2.x * h.y, h = j.position.y + b.col1.y * h.x + b.col2.y * h.y, this.m_points[n].x = x + 0.5 * (a - (x - o) * d - (h - u) * q - v) * d, this.m_points[n].y = h + 0.5 * (a - (x - o) * d - (h - u) * q - v) * q;
                            break;
                        case C.e_faceB:
                            b = j.R;
                            h = e.m_localPlaneNormal;
                            d = b.col1.x * h.x + b.col2.x * h.y;
                            q = b.col1.y * h.x + b.col2.y * h.y;
                            b = j.R;
                            h = e.m_localPoint;
                            o = j.position.x + b.col1.x * h.x + b.col2.x * h.y;
                            u = j.position.y + b.col1.y * h.x + b.col2.y * h.y;
                            this.m_normal.x = -d;
                            this.m_normal.y = -q;
                            for (n = 0; n < e.m_pointCount; n++) b = k.R, h = e.m_points[n].m_localPoint, x = k.position.x + b.col1.x * h.x + b.col2.x * h.y, h = k.position.y + b.col1.y * h.x + b.col2.y * h.y, this.m_points[n].x = x + 0.5 * (v - (x - o) * d - (h - u) * q - a) * d, this.m_points[n].y = h + 0.5 * (v - (x - o) * d - (h - u) * q - a) * q
                    }
                }
            };
            e.ClipVertex = function () {
                this.v = new d;
                this.id = new I
            };
            e.prototype.Set = function (e) {
                this.v.SetV(e.v);
                this.id.Set(e.id)
            };
            k.Features = function () {};
            Object.defineProperty(k.prototype, "referenceEdge", {
                enumerable: !1,
                configurable: !0,
                get: function () {
                    return this._referenceEdge
                }
            });
            Object.defineProperty(k.prototype, "referenceEdge", {
                enumerable: !1,
                configurable: !0,
                set: function (e) {
                    e === void 0 && (e = 0);
                    this._referenceEdge = e;
                    this._m_id._key = this._m_id._key & 4294967040 | this._referenceEdge & 255
                }
            });
            Object.defineProperty(k.prototype, "incidentEdge", {
                enumerable: !1,
                configurable: !0,
                get: function () {
                    return this._incidentEdge
                }
            });
            Object.defineProperty(k.prototype, "incidentEdge", {
                enumerable: !1,
                configurable: !0,
                set: function (e) {
                    e === void 0 && (e = 0);
                    this._incidentEdge = e;
                    this._m_id._key = this._m_id._key & 4294902015 | this._incidentEdge << 8 & 65280
                }
            });
            Object.defineProperty(k.prototype, "incidentVertex", {
                enumerable: !1,
                configurable: !0,
                get: function () {
                    return this._incidentVertex
                }
            });
            Object.defineProperty(k.prototype, "incidentVertex", {
                enumerable: !1,
                configurable: !0,
                set: function (e) {
                    e === void 0 && (e = 0);
                    this._incidentVertex = e;
                    this._m_id._key = this._m_id._key & 4278255615 | this._incidentVertex << 16 & 16711680
                }
            });
            Object.defineProperty(k.prototype, "flip", {
                enumerable: !1,
                configurable: !0,
                get: function () {
                    return this._flip
                }
            });
            Object.defineProperty(k.prototype, "flip", {
                enumerable: !1,
                configurable: !0,
                set: function (e) {
                    e === void 0 && (e = 0);
                    this._flip = e;
                    this._m_id._key = this._m_id._key & 16777215 | this._flip << 24 & 4278190080
                }
            })
        })();
        (function () {
            var b = a.Common.b2Settings,
                c = a.Collision.Shapes.b2CircleShape,
                g = a.Collision.Shapes.b2EdgeChainDef,
                p = a.Collision.Shapes.b2EdgeShape,
                f = a.Collision.Shapes.b2MassData,
                l = a.Collision.Shapes.b2PolygonShape,
                m = a.Collision.Shapes.b2Shape,
                d = a.Common.Math.b2Mat22,
                t = a.Common.Math.b2Math,
                i = a.Common.Math.b2Transform,
                B = a.Common.Math.b2Vec2,
                F = a.Collision.b2Distance,
                I = a.Collision.b2DistanceInput,
                w = a.Collision.b2DistanceOutput,
                Q = a.Collision.b2DistanceProxy,
                y = a.Collision.b2SimplexCache;
            a.inherit(c, a.Collision.Shapes.b2Shape);
            c.prototype.__super = a.Collision.Shapes.b2Shape.prototype;
            c.b2CircleShape = function () {
                a.Collision.Shapes.b2Shape.b2Shape.apply(this,
                arguments);
                this.m_p = new B
            };
            c.prototype.Copy = function () {
                var a = new c;
                a.Set(this);
                return a
            };
            c.prototype.Set = function (b) {
                this.__super.Set.call(this, b);
                a.is(b, c) && this.m_p.SetV((b instanceof c ? b : null).m_p)
            };
            c.prototype.TestPoint = function (a, b) {
                var d = a.R,
                    c = a.position.x + (d.col1.x * this.m_p.x + d.col2.x * this.m_p.y),
                    d = a.position.y + (d.col1.y * this.m_p.x + d.col2.y * this.m_p.y),
                    c = b.x - c,
                    d = b.y - d;
                return c * c + d * d <= this.m_radius * this.m_radius
            };
            c.prototype.RayCast = function (a, b, d) {
                var c = d.R,
                    f = b.p1.x - (d.position.x + (c.col1.x * this.m_p.x + c.col2.x * this.m_p.y)),
                    d = b.p1.y - (d.position.y + (c.col1.y * this.m_p.x + c.col2.y * this.m_p.y)),
                    c = b.p2.x - b.p1.x,
                    g = b.p2.y - b.p1.y,
                    p = f * c + d * g,
                    t = c * c + g * g,
                    l = p * p - t * (f * f + d * d - this.m_radius * this.m_radius);
                if (l < 0 || t < Number.MIN_VALUE) return !1;
                p = -(p + Math.sqrt(l));
                if (0 <= p && p <= b.maxFraction * t) return p /= t, a.fraction = p, a.normal.x = f + p * c, a.normal.y = d + p * g, a.normal.Normalize(), !0;
                return !1
            };
            c.prototype.ComputeAABB = function (a, b) {
                var d = b.R,
                    c = b.position.x + (d.col1.x * this.m_p.x + d.col2.x * this.m_p.y),
                    d = b.position.y + (d.col1.y * this.m_p.x + d.col2.y * this.m_p.y);
                a.lowerBound.Set(c - this.m_radius, d - this.m_radius);
                a.upperBound.Set(c + this.m_radius, d + this.m_radius)
            };
            c.prototype.ComputeMass = function (a, d) {
                d === void 0 && (d = 0);
                a.mass = d * b.b2_pi * this.m_radius * this.m_radius;
                a.center.SetV(this.m_p);
                a.I = a.mass * (0.5 * this.m_radius * this.m_radius + (this.m_p.x * this.m_p.x + this.m_p.y * this.m_p.y))
            };
            c.prototype.ComputeSubmergedArea = function (a, b, d, c) {
                b === void 0 && (b = 0);
                var d = t.MulX(d, this.m_p),
                    f = -(t.Dot(a, d) - b);
                if (f < -this.m_radius + Number.MIN_VALUE) return 0;
                if (f > this.m_radius) return c.SetV(d), Math.PI * this.m_radius * this.m_radius;
                var b = this.m_radius * this.m_radius,
                    g = f * f,
                    f = b * (Math.asin(f / this.m_radius) + Math.PI / 2) + f * Math.sqrt(b - g),
                    b = -2 / 3 * Math.pow(b - g, 1.5) / f;
                c.x = d.x + a.x * b;
                c.y = d.y + a.y * b;
                return f
            };
            c.prototype.GetLocalPosition = function () {
                return this.m_p
            };
            c.prototype.SetLocalPosition = function (a) {
                this.m_p.SetV(a)
            };
            c.prototype.GetRadius = function () {
                return this.m_radius
            };
            c.prototype.SetRadius = function (a) {
                a === void 0 && (a = 0);
                this.m_radius = a
            };
            c.prototype.b2CircleShape = function (a) {
                a === void 0 && (a = 0);
                this.__super.b2Shape.call(this);
                this.m_type = m.e_circleShape;
                this.m_radius = a
            };
            g.b2EdgeChainDef = function () {};
            g.prototype.b2EdgeChainDef = function () {
                this.vertexCount = 0;
                this.isALoop = !0;
                this.vertices = []
            };
            a.inherit(p, a.Collision.Shapes.b2Shape);
            p.prototype.__super = a.Collision.Shapes.b2Shape.prototype;
            p.b2EdgeShape = function () {
                a.Collision.Shapes.b2Shape.b2Shape.apply(this, arguments);
                this.s_supportVec = new B;
                this.m_v1 = new B;
                this.m_v2 = new B;
                this.m_coreV1 = new B;
                this.m_coreV2 = new B;
                this.m_normal = new B;
                this.m_direction = new B;
                this.m_cornerDir1 = new B;
                this.m_cornerDir2 = new B
            };
            p.prototype.TestPoint = function () {
                return !1
            };
            p.prototype.RayCast = function (a, b, d) {
                var c, f = b.p2.x - b.p1.x,
                    g = b.p2.y - b.p1.y;
                c = d.R;
                var p = d.position.x + (c.col1.x * this.m_v1.x + c.col2.x * this.m_v1.y),
                    t = d.position.y + (c.col1.y * this.m_v1.x + c.col2.y * this.m_v1.y),
                    l = d.position.y + (c.col1.y * this.m_v2.x + c.col2.y * this.m_v2.y) - t,
                    d = -(d.position.x + (c.col1.x * this.m_v2.x + c.col2.x * this.m_v2.y) - p);
                c = 100 * Number.MIN_VALUE;
                var m = -(f * l + g * d);
                if (m > c) {
                    var p = b.p1.x - p,
                        i = b.p1.y - t,
                        t = p * l + i * d;
                    if (0 <= t && t <= b.maxFraction * m && (b = -f * i + g * p, -c * m <= b && b <= m * (1 + c))) return t /= m, a.fraction = t, b = Math.sqrt(l * l + d * d), a.normal.x = l / b, a.normal.y = d / b, !0
                }
                return !1
            };
            p.prototype.ComputeAABB = function (a, b) {
                var d = b.R,
                    c = b.position.x + (d.col1.x * this.m_v1.x + d.col2.x * this.m_v1.y),
                    f = b.position.y + (d.col1.y * this.m_v1.x + d.col2.y * this.m_v1.y),
                    g = b.position.x + (d.col1.x * this.m_v2.x + d.col2.x * this.m_v2.y),
                    d = b.position.y + (d.col1.y * this.m_v2.x + d.col2.y * this.m_v2.y);
                c < g ? (a.lowerBound.x = c,
                a.upperBound.x = g) : (a.lowerBound.x = g, a.upperBound.x = c);
                f < d ? (a.lowerBound.y = f, a.upperBound.y = d) : (a.lowerBound.y = d, a.upperBound.y = f)
            };
            p.prototype.ComputeMass = function (a) {
                a.mass = 0;
                a.center.SetV(this.m_v1);
                a.I = 0
            };
            p.prototype.ComputeSubmergedArea = function (a, b, d, c) {
                b === void 0 && (b = 0);
                var f = new B(a.x * b, a.y * b),
                    g = t.MulX(d, this.m_v1),
                    d = t.MulX(d, this.m_v2),
                    p = t.Dot(a, g) - b,
                    a = t.Dot(a, d) - b;
                if (p > 0) if (a > 0) return 0;
                else g.x = -a / (p - a) * g.x + p / (p - a) * d.x, g.y = -a / (p - a) * g.y + p / (p - a) * d.y;
                else if (a > 0) d.x = -a / (p - a) * g.x + p / (p - a) * d.x,
                d.y = -a / (p - a) * g.y + p / (p - a) * d.y;
                c.x = (f.x + g.x + d.x) / 3;
                c.y = (f.y + g.y + d.y) / 3;
                return 0.5 * ((g.x - f.x) * (d.y - f.y) - (g.y - f.y) * (d.x - f.x))
            };
            p.prototype.GetLength = function () {
                return this.m_length
            };
            p.prototype.GetVertex1 = function () {
                return this.m_v1
            };
            p.prototype.GetVertex2 = function () {
                return this.m_v2
            };
            p.prototype.GetCoreVertex1 = function () {
                return this.m_coreV1
            };
            p.prototype.GetCoreVertex2 = function () {
                return this.m_coreV2
            };
            p.prototype.GetNormalVector = function () {
                return this.m_normal
            };
            p.prototype.GetDirectionVector = function () {
                return this.m_direction
            };
            p.prototype.GetCorner1Vector = function () {
                return this.m_cornerDir1
            };
            p.prototype.GetCorner2Vector = function () {
                return this.m_cornerDir2
            };
            p.prototype.Corner1IsConvex = function () {
                return this.m_cornerConvex1
            };
            p.prototype.Corner2IsConvex = function () {
                return this.m_cornerConvex2
            };
            p.prototype.GetFirstVertex = function (a) {
                var b = a.R;
                return new B(a.position.x + (b.col1.x * this.m_coreV1.x + b.col2.x * this.m_coreV1.y), a.position.y + (b.col1.y * this.m_coreV1.x + b.col2.y * this.m_coreV1.y))
            };
            p.prototype.GetNextEdge = function () {
                return this.m_nextEdge
            };
            p.prototype.GetPrevEdge = function () {
                return this.m_prevEdge
            };
            p.prototype.Support = function (a, b, d) {
                b === void 0 && (b = 0);
                d === void 0 && (d = 0);
                var c = a.R,
                    f = a.position.x + (c.col1.x * this.m_coreV1.x + c.col2.x * this.m_coreV1.y),
                    g = a.position.y + (c.col1.y * this.m_coreV1.x + c.col2.y * this.m_coreV1.y),
                    p = a.position.x + (c.col1.x * this.m_coreV2.x + c.col2.x * this.m_coreV2.y),
                    a = a.position.y + (c.col1.y * this.m_coreV2.x + c.col2.y * this.m_coreV2.y);
                f * b + g * d > p * b + a * d ? (this.s_supportVec.x = f, this.s_supportVec.y = g) : (this.s_supportVec.x = p, this.s_supportVec.y = a);
                return this.s_supportVec
            };
            p.prototype.b2EdgeShape = function (a, d) {
                this.__super.b2Shape.call(this);
                this.m_type = m.e_edgeShape;
                this.m_nextEdge = this.m_prevEdge = null;
                this.m_v1 = a;
                this.m_v2 = d;
                this.m_direction.Set(this.m_v2.x - this.m_v1.x, this.m_v2.y - this.m_v1.y);
                this.m_length = this.m_direction.Normalize();
                this.m_normal.Set(this.m_direction.y, -this.m_direction.x);
                this.m_coreV1.Set(-b.b2_toiSlop * (this.m_normal.x - this.m_direction.x) + this.m_v1.x, -b.b2_toiSlop * (this.m_normal.y - this.m_direction.y) + this.m_v1.y);
                this.m_coreV2.Set(-b.b2_toiSlop * (this.m_normal.x + this.m_direction.x) + this.m_v2.x, -b.b2_toiSlop * (this.m_normal.y + this.m_direction.y) + this.m_v2.y);
                this.m_cornerDir1 = this.m_normal;
                this.m_cornerDir2.Set(-this.m_normal.x, -this.m_normal.y)
            };
            p.prototype.SetPrevEdge = function (a, b, d, c) {
                this.m_prevEdge = a;
                this.m_coreV1 = b;
                this.m_cornerDir1 = d;
                this.m_cornerConvex1 = c
            };
            p.prototype.SetNextEdge = function (a, b, d, c) {
                this.m_nextEdge = a;
                this.m_coreV2 = b;
                this.m_cornerDir2 = d;
                this.m_cornerConvex2 = c
            };
            f.b2MassData = function () {
                this.mass = 0;
                this.center = new B(0, 0);
                this.I = 0
            };
            a.inherit(l, a.Collision.Shapes.b2Shape);
            l.prototype.__super = a.Collision.Shapes.b2Shape.prototype;
            l.b2PolygonShape = function () {
                a.Collision.Shapes.b2Shape.b2Shape.apply(this, arguments)
            };
            l.prototype.Copy = function () {
                var a = new l;
                a.Set(this);
                return a
            };
            l.prototype.Set = function (b) {
                this.__super.Set.call(this, b);
                if (a.is(b, l)) {
                    b = b instanceof l ? b : null;
                    this.m_centroid.SetV(b.m_centroid);
                    this.m_vertexCount = b.m_vertexCount;
                    this.Reserve(this.m_vertexCount);
                    for (var d = 0; d < this.m_vertexCount; d++) this.m_vertices[d].SetV(b.m_vertices[d]),
                    this.m_normals[d].SetV(b.m_normals[d])
                }
            };
            l.prototype.SetAsArray = function (a, b) {
                b === void 0 && (b = 0);
                for (var d = new s, c = 0, f, c = 0; c < a.length; ++c) f = a[c], d.push(f);
                this.SetAsVector(d, b)
            };
            l.AsArray = function (a, b) {
                b === void 0 && (b = 0);
                var d = new l;
                d.SetAsArray(a, b);
                return d
            };
            l.prototype.SetAsVector = function (a, d) {
                d === void 0 && (d = 0);
                if (d == 0) d = a.length;
                b.b2Assert(2 <= d);
                this.m_vertexCount = d;
                this.Reserve(d);
                for (var c = 0, c = 0; c < this.m_vertexCount; c++) this.m_vertices[c].SetV(a[c]);
                for (c = 0; c < this.m_vertexCount; ++c) {
                    var f = parseInt(c),
                        g = parseInt(c + 1 < this.m_vertexCount ? c + 1 : 0),
                        f = t.SubtractVV(this.m_vertices[g], this.m_vertices[f]);
                    b.b2Assert(f.LengthSquared() > Number.MIN_VALUE);
                    this.m_normals[c].SetV(t.CrossVF(f, 1));
                    this.m_normals[c].Normalize()
                }
                this.m_centroid = l.ComputeCentroid(this.m_vertices, this.m_vertexCount)
            };
            l.AsVector = function (a, b) {
                b === void 0 && (b = 0);
                var d = new l;
                d.SetAsVector(a, b);
                return d
            };
            l.prototype.SetAsBox = function (a, b) {
                a === void 0 && (a = 0);
                b === void 0 && (b = 0);
                this.m_vertexCount = 4;
                this.Reserve(4);
                this.m_vertices[0].Set(-a, -b);
                this.m_vertices[1].Set(a, -b);
                this.m_vertices[2].Set(a, b);
                this.m_vertices[3].Set(-a, b);
                this.m_normals[0].Set(0, -1);
                this.m_normals[1].Set(1, 0);
                this.m_normals[2].Set(0, 1);
                this.m_normals[3].Set(-1, 0);
                this.m_centroid.SetZero()
            };
            l.AsBox = function (a, b) {
                a === void 0 && (a = 0);
                b === void 0 && (b = 0);
                var d = new l;
                d.SetAsBox(a, b);
                return d
            };
            l.prototype.SetAsOrientedBox = function (a, b, d, c) {
                a === void 0 && (a = 0);
                b === void 0 && (b = 0);
                d === void 0 && (d = null);
                c === void 0 && (c = 0);
                this.m_vertexCount = 4;
                this.Reserve(4);
                this.m_vertices[0].Set(-a, -b);
                this.m_vertices[1].Set(a, -b);
                this.m_vertices[2].Set(a, b);
                this.m_vertices[3].Set(-a, b);
                this.m_normals[0].Set(0, -1);
                this.m_normals[1].Set(1, 0);
                this.m_normals[2].Set(0, 1);
                this.m_normals[3].Set(-1, 0);
                this.m_centroid = d;
                a = new i;
                a.position = d;
                a.R.Set(c);
                for (d = 0; d < this.m_vertexCount; ++d) this.m_vertices[d] = t.MulX(a, this.m_vertices[d]), this.m_normals[d] = t.MulMV(a.R, this.m_normals[d])
            };
            l.AsOrientedBox = function (a, b, d, c) {
                a === void 0 && (a = 0);
                b === void 0 && (b = 0);
                d === void 0 && (d = null);
                c === void 0 && (c = 0);
                var f = new l;
                f.SetAsOrientedBox(a, b, d, c);
                return f
            };
            l.prototype.SetAsEdge = function (a, b) {
                this.m_vertexCount = 2;
                this.Reserve(2);
                this.m_vertices[0].SetV(a);
                this.m_vertices[1].SetV(b);
                this.m_centroid.x = 0.5 * (a.x + b.x);
                this.m_centroid.y = 0.5 * (a.y + b.y);
                this.m_normals[0] = t.CrossVF(t.SubtractVV(b, a), 1);
                this.m_normals[0].Normalize();
                this.m_normals[1].x = -this.m_normals[0].x;
                this.m_normals[1].y = -this.m_normals[0].y
            };
            l.AsEdge = function (a, b) {
                var d = new l;
                d.SetAsEdge(a, b);
                return d
            };
            l.prototype.TestPoint = function (a, b) {
                var d;
                d = a.R;
                for (var c = b.x - a.position.x, f = b.y - a.position.y, g = c * d.col1.x + f * d.col1.y, p = c * d.col2.x + f * d.col2.y, t = 0; t < this.m_vertexCount; ++t) if (d = this.m_vertices[t], c = g - d.x, f = p - d.y, d = this.m_normals[t], d.x * c + d.y * f > 0) return !1;
                return !0
            };
            l.prototype.RayCast = function (a, b, d) {
                var c = 0,
                    f = b.maxFraction,
                    g = 0,
                    p = 0,
                    t, l, g = b.p1.x - d.position.x,
                    p = b.p1.y - d.position.y;
                t = d.R;
                var m = g * t.col1.x + p * t.col1.y,
                    i = g * t.col2.x + p * t.col2.y,
                    g = b.p2.x - d.position.x,
                    p = b.p2.y - d.position.y;
                t = d.R;
                b = g * t.col1.x + p * t.col1.y - m;
                t = g * t.col2.x + p * t.col2.y - i;
                for (var r = parseInt(-1), j = 0; j < this.m_vertexCount; ++j) {
                    l = this.m_vertices[j];
                    g = l.x - m;
                    p = l.y - i;
                    l = this.m_normals[j];
                    g = l.x * g + l.y * p;
                    p = l.x * b + l.y * t;
                    if (p == 0) {
                        if (g < 0) return !1
                    } else p < 0 && g < c * p ? (c = g / p, r = j) : p > 0 && g < f * p && (f = g / p);
                    if (f < c - Number.MIN_VALUE) return !1
                }
                if (r >= 0) return a.fraction = c, t = d.R, l = this.m_normals[r], a.normal.x = t.col1.x * l.x + t.col2.x * l.y, a.normal.y = t.col1.y * l.x + t.col2.y * l.y, !0;
                return !1
            };
            l.prototype.ComputeAABB = function (a, b) {
                for (var d = b.R, c = this.m_vertices[0], f = b.position.x + (d.col1.x * c.x + d.col2.x * c.y), g = b.position.y + (d.col1.y * c.x + d.col2.y * c.y), p = f, t = g, l = 1; l < this.m_vertexCount; ++l) var c = this.m_vertices[l],
                    m = b.position.x + (d.col1.x * c.x + d.col2.x * c.y),
                    c = b.position.y + (d.col1.y * c.x + d.col2.y * c.y),
                    f = f < m ? f : m,
                    g = g < c ? g : c,
                    p = p > m ? p : m,
                    t = t > c ? t : c;
                a.lowerBound.x = f - this.m_radius;
                a.lowerBound.y = g - this.m_radius;
                a.upperBound.x = p + this.m_radius;
                a.upperBound.y = t + this.m_radius
            };
            l.prototype.ComputeMass = function (a, b) {
                b === void 0 && (b = 0);
                if (this.m_vertexCount == 2) a.center.x = 0.5 * (this.m_vertices[0].x + this.m_vertices[1].x), a.center.y = 0.5 * (this.m_vertices[0].y + this.m_vertices[1].y), a.mass = 0, a.I = 0;
                else {
                    for (var d = 0, c = 0, f = 0, g = 0, p = 1 / 3, t = 0; t < this.m_vertexCount; ++t) {
                        var l = this.m_vertices[t],
                            m = t + 1 < this.m_vertexCount ? this.m_vertices[parseInt(t + 1)] : this.m_vertices[0],
                            i = l.x - 0,
                            r = l.y - 0,
                            j = m.x - 0,
                            n = m.y - 0,
                            q = i * n - r * j,
                            h = 0.5 * q;
                        f += h;
                        d += h * p * (0 + l.x + m.x);
                        c += h * p * (0 + l.y + m.y);
                        l = i;
                        g += q * (p * (0.25 * (l * l + j * l + j * j) + (0 * l + 0 * j)) + 0 + (p * (0.25 * (r * r + n * r + n * n) + (0 * r + 0 * n)) + 0))
                    }
                    a.mass = b * f;
                    d *= 1 / f;
                    c *= 1 / f;
                    a.center.Set(d, c);
                    a.I = b * g
                }
            };
            l.prototype.ComputeSubmergedArea = function (a, b, d, c) {
                b === void 0 && (b = 0);
                for (var g = t.MulTMV(d.R, a), p = b - t.Dot(a, d.position), l = new r, m = 0, i = parseInt(-1), b = parseInt(-1), A = !1, a = a = 0; a < this.m_vertexCount; ++a) {
                    l[a] = t.Dot(g, this.m_vertices[a]) - p;
                    var z = l[a] < -Number.MIN_VALUE;
                    a > 0 && (z ? A || (i = a - 1, m++) : A && (b = a - 1, m++));
                    A = z
                }
                switch (m) {
                    case 0:
                        return A ? (a = new f, this.ComputeMass(a, 1), c.SetV(t.MulX(d, a.center)), a.mass) : 0;
                    case 1:
                        i == -1 ? i = this.m_vertexCount - 1 : b = this.m_vertexCount - 1
                }
                a = parseInt((i + 1) % this.m_vertexCount);
                g = parseInt((b + 1) % this.m_vertexCount);
                p = (0 - l[i]) / (l[a] - l[i]);
                l = (0 - l[b]) / (l[g] - l[b]);
                i = new B(this.m_vertices[i].x * (1 - p) + this.m_vertices[a].x * p, this.m_vertices[i].y * (1 - p) + this.m_vertices[a].y * p);
                b = new B(this.m_vertices[b].x * (1 - l) + this.m_vertices[g].x * l, this.m_vertices[b].y * (1 - l) + this.m_vertices[g].y * l);
                l = 0;
                p = new B;
                for (m = this.m_vertices[a]; a != g;) a = (a + 1) % this.m_vertexCount, A = a == g ? b : this.m_vertices[a], z = 0.5 * ((m.x - i.x) * (A.y - i.y) - (m.y - i.y) * (A.x - i.x)), l += z, p.x += z * (i.x + m.x + A.x) / 3, p.y += z * (i.y + m.y + A.y) / 3, m = A;
                p.Multiply(1 / l);
                c.SetV(t.MulX(d, p));
                return l
            };
            l.prototype.GetVertexCount = function () {
                return this.m_vertexCount
            };
            l.prototype.GetVertices = function () {
                return this.m_vertices
            };
            l.prototype.GetNormals = function () {
                return this.m_normals
            };
            l.prototype.GetSupport = function (a) {
                for (var b = 0, d = this.m_vertices[0].x * a.x + this.m_vertices[0].y * a.y, c = 1; c < this.m_vertexCount; ++c) {
                    var f = this.m_vertices[c].x * a.x + this.m_vertices[c].y * a.y;
                    f > d && (b = c, d = f)
                }
                return b
            };
            l.prototype.GetSupportVertex = function (a) {
                for (var b = 0, d = this.m_vertices[0].x * a.x + this.m_vertices[0].y * a.y, c = 1; c < this.m_vertexCount; ++c) {
                    var f = this.m_vertices[c].x * a.x + this.m_vertices[c].y * a.y;
                    f > d && (b = c, d = f)
                }
                return this.m_vertices[b]
            };
            l.prototype.Validate = function () {
                return !1
            };
            l.prototype.b2PolygonShape = function () {
                this.__super.b2Shape.call(this);
                this.m_type = m.e_polygonShape;
                this.m_centroid = new B;
                this.m_vertices = new s;
                this.m_normals = new s
            };
            l.prototype.Reserve = function (a) {
                a === void 0 && (a = 0);
                for (var b = parseInt(this.m_vertices.length); b < a; b++) this.m_vertices[b] = new B, this.m_normals[b] = new B
            };
            l.ComputeCentroid = function (a, b) {
                b === void 0 && (b = 0);
                for (var d = new B, c = 0, f = 1 / 3, g = 0; g < b; ++g) {
                    var p = a[g],
                        t = g + 1 < b ? a[parseInt(g + 1)] : a[0],
                        l = 0.5 * ((p.x - 0) * (t.y - 0) - (p.y - 0) * (t.x - 0));
                    c += l;
                    d.x += l * f * (0 + p.x + t.x);
                    d.y += l * f * (0 + p.y + t.y)
                }
                d.x *= 1 / c;
                d.y *= 1 / c;
                return d
            };
            l.ComputeOBB = function (a, b, d) {
                d === void 0 && (d = 0);
                for (var c = 0, f = new s(d + 1), c = 0; c < d; ++c) f[c] = b[c];
                f[d] = f[0];
                b = Number.MAX_VALUE;
                for (c = 1; c <= d; ++c) {
                    var g = f[parseInt(c - 1)],
                        p = f[c].x - g.x,
                        t = f[c].y - g.y,
                        l = Math.sqrt(p * p + t * t);
                    p /= l;
                    t /= l;
                    for (var m = -t, i = p, r = l = Number.MAX_VALUE, j = -Number.MAX_VALUE, n = -Number.MAX_VALUE, q = 0; q < d; ++q) {
                        var h = f[q].x - g.x,
                            u = f[q].y - g.y,
                            G = p * h + t * u,
                            h = m * h + i * u;
                        G < l && (l = G);
                        h < r && (r = h);
                        G > j && (j = G);
                        h > n && (n = h)
                    }
                    q = (j - l) * (n - r);
                    if (q < 0.95 * b) b = q, a.R.col1.x = p, a.R.col1.y = t, a.R.col2.x = m, a.R.col2.y = i, p = 0.5 * (l + j), t = 0.5 * (r + n), m = a.R, a.center.x = g.x + (m.col1.x * p + m.col2.x * t), a.center.y = g.y + (m.col1.y * p + m.col2.y * t), a.extents.x = 0.5 * (j - l), a.extents.y = 0.5 * (n - r)
                }
            };
            a.postDefs.push(function () {
                a.Collision.Shapes.b2PolygonShape.s_mat = new d
            });
            m.b2Shape = function () {};
            m.prototype.Copy = function () {
                return null
            };
            m.prototype.Set = function (a) {
                this.m_radius = a.m_radius
            };
            m.prototype.GetType = function () {
                return this.m_type
            };
            m.prototype.TestPoint = function () {
                return !1
            };
            m.prototype.RayCast = function () {
                return !1
            };
            m.prototype.ComputeAABB = function () {};
            m.prototype.ComputeMass = function () {};
            m.prototype.ComputeSubmergedArea = function () {
                return 0
            };
            m.TestOverlap = function (a, b, d, c) {
                var f = new I;
                f.proxyA = new Q;
                f.proxyA.Set(a);
                f.proxyB = new Q;
                f.proxyB.Set(d);
                f.transformA = b;
                f.transformB = c;
                f.useRadii = !0;
                a = new y;
                a.count = 0;
                b = new w;
                F.Distance(b, a, f);
                return b.distance < 10 * Number.MIN_VALUE
            };
            m.prototype.b2Shape = function () {
                this.m_type = m.e_unknownShape;
                this.m_radius = b.b2_linearSlop
            };
            a.postDefs.push(function () {
                a.Collision.Shapes.b2Shape.e_unknownShape = parseInt(-1);
                a.Collision.Shapes.b2Shape.e_circleShape = 0;
                a.Collision.Shapes.b2Shape.e_polygonShape = 1;
                a.Collision.Shapes.b2Shape.e_edgeShape = 2;
                a.Collision.Shapes.b2Shape.e_shapeTypeCount = 3;
                a.Collision.Shapes.b2Shape.e_hitCollide = 1;
                a.Collision.Shapes.b2Shape.e_missCollide = 0;
                a.Collision.Shapes.b2Shape.e_startsInsideCollide = parseInt(-1)
            })
        })();
        (function () {
            var b = a.Common.b2Color,
                c = a.Common.b2Settings,
                g = a.Common.Math.b2Math;
            b.b2Color = function () {
                this._b = this._g = this._r = 0
            };
            b.prototype.b2Color = function (b, c, l) {
                b === void 0 && (b = 0);
                c === void 0 && (c = 0);
                l === void 0 && (l = 0);
                this._r = a.parseUInt(255 * g.Clamp(b, 0, 1));
                this._g = a.parseUInt(255 * g.Clamp(c, 0, 1));
                this._b = a.parseUInt(255 * g.Clamp(l, 0, 1))
            };
            b.prototype.Set = function (b, c, l) {
                b === void 0 && (b = 0);
                c === void 0 && (c = 0);
                l === void 0 && (l = 0);
                this._r = a.parseUInt(255 * g.Clamp(b, 0, 1));
                this._g = a.parseUInt(255 * g.Clamp(c, 0, 1));
                this._b = a.parseUInt(255 * g.Clamp(l, 0, 1))
            };
            Object.defineProperty(b.prototype, "r", {
                enumerable: !1,
                configurable: !0,
                set: function (b) {
                    b === void 0 && (b = 0);
                    this._r = a.parseUInt(255 * g.Clamp(b, 0, 1))
                }
            });
            Object.defineProperty(b.prototype, "g", {
                enumerable: !1,
                configurable: !0,
                set: function (b) {
                    b === void 0 && (b = 0);
                    this._g = a.parseUInt(255 * g.Clamp(b, 0, 1))
                }
            });
            Object.defineProperty(b.prototype, "b", {
                enumerable: !1,
                configurable: !0,
                set: function (b) {
                    b === void 0 && (b = 0);
                    this._b = a.parseUInt(255 * g.Clamp(b, 0, 1))
                }
            });
            Object.defineProperty(b.prototype, "color", {
                enumerable: !1,
                configurable: !0,
                get: function () {
                    return this._r << 16 | this._g << 8 | this._b
                }
            });
            c.b2Settings = function () {};
            c.b2MixFriction = function (a, b) {
                a === void 0 && (a = 0);
                b === void 0 && (b = 0);
                return Math.sqrt(a * b)
            };
            c.b2MixRestitution = function (a, b) {
                a === void 0 && (a = 0);
                b === void 0 && (b = 0);
                return a > b ? a : b
            };
            c.b2Assert = function (a) {
                if (!a) throw "Assertion Failed";
            };
            a.postDefs.push(function () {
                a.Common.b2Settings.VERSION = "2.1alpha";
                a.Common.b2Settings.USHRT_MAX = 65535;
                a.Common.b2Settings.b2_pi = Math.PI;
                a.Common.b2Settings.b2_maxManifoldPoints = 2;
                a.Common.b2Settings.b2_aabbExtension = 0.1;
                a.Common.b2Settings.b2_aabbMultiplier = 2;
                a.Common.b2Settings.b2_polygonRadius = 2 * c.b2_linearSlop;
                a.Common.b2Settings.b2_linearSlop = 0.0050;
                a.Common.b2Settings.b2_angularSlop = 2 / 180 * c.b2_pi;
                a.Common.b2Settings.b2_toiSlop = 8 * c.b2_linearSlop;
                a.Common.b2Settings.b2_maxTOIContactsPerIsland = 32;
                a.Common.b2Settings.b2_maxTOIJointsPerIsland = 32;
                a.Common.b2Settings.b2_velocityThreshold = 1;
                a.Common.b2Settings.b2_maxLinearCorrection = 0.2;
                a.Common.b2Settings.b2_maxAngularCorrection = 8 / 180 * c.b2_pi;
                a.Common.b2Settings.b2_maxTranslation = 2;
                a.Common.b2Settings.b2_maxTranslationSquared = c.b2_maxTranslation * c.b2_maxTranslation;
                a.Common.b2Settings.b2_maxRotation = 0.5 * c.b2_pi;
                a.Common.b2Settings.b2_maxRotationSquared = c.b2_maxRotation * c.b2_maxRotation;
                a.Common.b2Settings.b2_contactBaumgarte = 0.2;
                a.Common.b2Settings.b2_timeToSleep = 0.5;
                a.Common.b2Settings.b2_linearSleepTolerance = 0.01;
                a.Common.b2Settings.b2_angularSleepTolerance = 2 / 180 * c.b2_pi
            })
        })();
        (function () {
            var b = a.Common.Math.b2Mat22,
                c = a.Common.Math.b2Mat33,
                g = a.Common.Math.b2Math,
                p = a.Common.Math.b2Sweep,
                f = a.Common.Math.b2Transform,
                l = a.Common.Math.b2Vec2,
                m = a.Common.Math.b2Vec3;
            b.b2Mat22 = function () {
                this.col1 = new l;
                this.col2 = new l
            };
            b.prototype.b2Mat22 = function () {
                this.SetIdentity()
            };
            b.FromAngle = function (a) {
                a === void 0 && (a = 0);
                var c = new b;
                c.Set(a);
                return c
            };
            b.FromVV = function (a, c) {
                var g = new b;
                g.SetVV(a, c);
                return g
            };
            b.prototype.Set = function (a) {
                a === void 0 && (a = 0);
                var b = Math.cos(a),
                    a = Math.sin(a);
                this.col1.x = b;
                this.col2.x = -a;
                this.col1.y = a;
                this.col2.y = b
            };
            b.prototype.SetVV = function (a, b) {
                this.col1.SetV(a);
                this.col2.SetV(b)
            };
            b.prototype.Copy = function () {
                var a = new b;
                a.SetM(this);
                return a
            };
            b.prototype.SetM = function (a) {
                this.col1.SetV(a.col1);
                this.col2.SetV(a.col2)
            };
            b.prototype.AddM = function (a) {
                this.col1.x += a.col1.x;
                this.col1.y += a.col1.y;
                this.col2.x += a.col2.x;
                this.col2.y += a.col2.y
            };
            b.prototype.SetIdentity = function () {
                this.col1.x = 1;
                this.col2.x = 0;
                this.col1.y = 0;
                this.col2.y = 1
            };
            b.prototype.SetZero = function () {
                this.col1.x = 0;
                this.col2.x = 0;
                this.col1.y = 0;
                this.col2.y = 0
            };
            b.prototype.GetAngle = function () {
                return Math.atan2(this.col1.y, this.col1.x)
            };
            b.prototype.GetInverse = function (a) {
                var b = this.col1.x,
                    c = this.col2.x,
                    g = this.col1.y,
                    f = this.col2.y,
                    p = b * f - c * g;
                p != 0 && (p = 1 / p);
                a.col1.x = p * f;
                a.col2.x = -p * c;
                a.col1.y = -p * g;
                a.col2.y = p * b;
                return a
            };
            b.prototype.Solve = function (a, b, c) {
                b === void 0 && (b = 0);
                c === void 0 && (c = 0);
                var g = this.col1.x,
                    f = this.col2.x,
                    p = this.col1.y,
                    l = this.col2.y,
                    m = g * l - f * p;
                m != 0 && (m = 1 / m);
                a.x = m * (l * b - f * c);
                a.y = m * (g * c - p * b);
                return a
            };
            b.prototype.Abs = function () {
                this.col1.Abs();
                this.col2.Abs()
            };
            c.b2Mat33 = function () {
                this.col1 = new m;
                this.col2 = new m;
                this.col3 = new m
            };
            c.prototype.b2Mat33 = function (a, b, c) {
                a === void 0 && (a = null);
                b === void 0 && (b = null);
                c === void 0 && (c = null);
                !a && !b && !c ? (this.col1.SetZero(), this.col2.SetZero(), this.col3.SetZero()) : (this.col1.SetV(a), this.col2.SetV(b), this.col3.SetV(c))
            };
            c.prototype.SetVVV = function (a, b, c) {
                this.col1.SetV(a);
                this.col2.SetV(b);
                this.col3.SetV(c)
            };
            c.prototype.Copy = function () {
                return new c(this.col1, this.col2, this.col3)
            };
            c.prototype.SetM = function (a) {
                this.col1.SetV(a.col1);
                this.col2.SetV(a.col2);
                this.col3.SetV(a.col3)
            };
            c.prototype.AddM = function (a) {
                this.col1.x += a.col1.x;
                this.col1.y += a.col1.y;
                this.col1.z += a.col1.z;
                this.col2.x += a.col2.x;
                this.col2.y += a.col2.y;
                this.col2.z += a.col2.z;
                this.col3.x += a.col3.x;
                this.col3.y += a.col3.y;
                this.col3.z += a.col3.z
            };
            c.prototype.SetIdentity = function () {
                this.col1.x = 1;
                this.col2.x = 0;
                this.col3.x = 0;
                this.col1.y = 0;
                this.col2.y = 1;
                this.col3.y = 0;
                this.col1.z = 0;
                this.col2.z = 0;
                this.col3.z = 1
            };
            c.prototype.SetZero = function () {
                this.col1.x = 0;
                this.col2.x = 0;
                this.col3.x = 0;
                this.col1.y = 0;
                this.col2.y = 0;
                this.col3.y = 0;
                this.col1.z = 0;
                this.col2.z = 0;
                this.col3.z = 0
            };
            c.prototype.Solve22 = function (a, b, c) {
                b === void 0 && (b = 0);
                c === void 0 && (c = 0);
                var g = this.col1.x,
                    f = this.col2.x,
                    p = this.col1.y,
                    l = this.col2.y,
                    m = g * l - f * p;
                m != 0 && (m = 1 / m);
                a.x = m * (l * b - f * c);
                a.y = m * (g * c - p * b);
                return a
            };
            c.prototype.Solve33 = function (a, b, c, g) {
                b === void 0 && (b = 0);
                c === void 0 && (c = 0);
                g === void 0 && (g = 0);
                var f = this.col1.x,
                    p = this.col1.y,
                    l = this.col1.z,
                    m = this.col2.x,
                    i = this.col2.y,
                    o = this.col2.z,
                    r = this.col3.x,
                    z = this.col3.y,
                    s = this.col3.z,
                    y = f * (i * s - o * z) + p * (o * r - m * s) + l * (m * z - i * r);
                y != 0 && (y = 1 / y);
                a.x = y * (b * (i * s - o * z) + c * (o * r - m * s) + g * (m * z - i * r));
                a.y = y * (f * (c * s - g * z) + p * (g * r - b * s) + l * (b * z - c * r));
                a.z = y * (f * (i * g - o * c) + p * (o * b - m * g) + l * (m * c - i * b));
                return a
            };
            g.b2Math = function () {};
            g.IsValid = function (a) {
                a === void 0 && (a = 0);
                return isFinite(a)
            };
            g.Dot = function (a, b) {
                return a.x * b.x + a.y * b.y
            };
            g.CrossVV = function (a, b) {
                return a.x * b.y - a.y * b.x
            };
            g.CrossVF = function (a, b) {
                b === void 0 && (b = 0);
                return new l(b * a.y, -b * a.x)
            };
            g.CrossFV = function (a, b) {
                a === void 0 && (a = 0);
                return new l(-a * b.y, a * b.x)
            };
            g.MulMV = function (a, b) {
                return new l(a.col1.x * b.x + a.col2.x * b.y, a.col1.y * b.x + a.col2.y * b.y)
            };
            g.MulTMV = function (a, b) {
                return new l(g.Dot(b, a.col1), g.Dot(b, a.col2))
            };
            g.MulX = function (a, b) {
                var c = g.MulMV(a.R, b);
                c.x += a.position.x;
                c.y += a.position.y;
                return c
            };
            g.MulXT = function (a, b) {
                var c = g.SubtractVV(b, a.position),
                    f = c.x * a.R.col1.x + c.y * a.R.col1.y;
                c.y = c.x * a.R.col2.x + c.y * a.R.col2.y;
                c.x = f;
                return c
            };
            g.AddVV = function (a, b) {
                return new l(a.x + b.x, a.y + b.y)
            };
            g.SubtractVV = function (a, b) {
                return new l(a.x - b.x, a.y - b.y)
            };
            g.Distance = function (a, b) {
                var c = a.x - b.x,
                    g = a.y - b.y;
                return Math.sqrt(c * c + g * g)
            };
            g.DistanceSquared = function (a, b) {
                var c = a.x - b.x,
                    g = a.y - b.y;
                return c * c + g * g
            };
            g.MulFV = function (a, b) {
                a === void 0 && (a = 0);
                return new l(a * b.x, a * b.y)
            };
            g.AddMM = function (a, c) {
                return b.FromVV(g.AddVV(a.col1, c.col1), g.AddVV(a.col2, c.col2))
            };
            g.MulMM = function (a, c) {
                return b.FromVV(g.MulMV(a, c.col1), g.MulMV(a, c.col2))
            };
            g.MulTMM = function (a, c) {
                var f = new l(g.Dot(a.col1, c.col1), g.Dot(a.col2, c.col1)),
                    p = new l(g.Dot(a.col1, c.col2), g.Dot(a.col2, c.col2));
                return b.FromVV(f, p)
            };
            g.Abs = function (a) {
                a === void 0 && (a = 0);
                return a > 0 ? a : -a
            };
            g.AbsV = function (a) {
                return new l(g.Abs(a.x),
                g.Abs(a.y))
            };
            g.AbsM = function (a) {
                return b.FromVV(g.AbsV(a.col1), g.AbsV(a.col2))
            };
            g.Min = function (a, b) {
                a === void 0 && (a = 0);
                b === void 0 && (b = 0);
                return a < b ? a : b
            };
            g.MinV = function (a, b) {
                return new l(g.Min(a.x, b.x), g.Min(a.y, b.y))
            };
            g.Max = function (a, b) {
                a === void 0 && (a = 0);
                b === void 0 && (b = 0);
                return a > b ? a : b
            };
            g.MaxV = function (a, b) {
                return new l(g.Max(a.x, b.x), g.Max(a.y, b.y))
            };
            g.Clamp = function (a, b, c) {
                a === void 0 && (a = 0);
                b === void 0 && (b = 0);
                c === void 0 && (c = 0);
                return a < b ? b : a > c ? c : a
            };
            g.ClampV = function (a, b, c) {
                return g.MaxV(b, g.MinV(a,
                c))
            };
            g.Swap = function (a, b) {
                var c = a[0];
                a[0] = b[0];
                b[0] = c
            };
            g.Random = function () {
                return Math.random() * 2 - 1
            };
            g.RandomRange = function (a, b) {
                a === void 0 && (a = 0);
                b === void 0 && (b = 0);
                var c = Math.random();
                return (b - a) * c + a
            };
            g.NextPowerOfTwo = function (a) {
                a === void 0 && (a = 0);
                a |= a >> 1 & 2147483647;
                a |= a >> 2 & 1073741823;
                a |= a >> 4 & 268435455;
                a |= a >> 8 & 16777215;
                a |= a >> 16 & 65535;
                return a + 1
            };
            g.IsPowerOfTwo = function (a) {
                a === void 0 && (a = 0);
                return a > 0 && (a & a - 1) == 0
            };
            a.postDefs.push(function () {
                a.Common.Math.b2Math.b2Vec2_zero = new l(0, 0);
                a.Common.Math.b2Math.b2Mat22_identity = b.FromVV(new l(1, 0), new l(0, 1));
                a.Common.Math.b2Math.b2Transform_identity = new f(g.b2Vec2_zero, g.b2Mat22_identity)
            });
            p.b2Sweep = function () {
                this.localCenter = new l;
                this.c0 = new l;
                this.c = new l
            };
            p.prototype.Set = function (a) {
                this.localCenter.SetV(a.localCenter);
                this.c0.SetV(a.c0);
                this.c.SetV(a.c);
                this.a0 = a.a0;
                this.a = a.a;
                this.t0 = a.t0
            };
            p.prototype.Copy = function () {
                var a = new p;
                a.localCenter.SetV(this.localCenter);
                a.c0.SetV(this.c0);
                a.c.SetV(this.c);
                a.a0 = this.a0;
                a.a = this.a;
                a.t0 = this.t0;
                return a
            };
            p.prototype.GetTransform = function (a, b) {
                b === void 0 && (b = 0);
                a.position.x = (1 - b) * this.c0.x + b * this.c.x;
                a.position.y = (1 - b) * this.c0.y + b * this.c.y;
                a.R.Set((1 - b) * this.a0 + b * this.a);
                var c = a.R;
                a.position.x -= c.col1.x * this.localCenter.x + c.col2.x * this.localCenter.y;
                a.position.y -= c.col1.y * this.localCenter.x + c.col2.y * this.localCenter.y
            };
            p.prototype.Advance = function (a) {
                a === void 0 && (a = 0);
                if (this.t0 < a && 1 - this.t0 > Number.MIN_VALUE) {
                    var b = (a - this.t0) / (1 - this.t0);
                    this.c0.x = (1 - b) * this.c0.x + b * this.c.x;
                    this.c0.y = (1 - b) * this.c0.y + b * this.c.y;
                    this.a0 = (1 - b) * this.a0 + b * this.a;
                    this.t0 = a
                }
            };
            f.b2Transform = function () {
                this.position = new l;
                this.R = new b
            };
            f.prototype.b2Transform = function (a, b) {
                a === void 0 && (a = null);
                b === void 0 && (b = null);
                a && (this.position.SetV(a), this.R.SetM(b))
            };
            f.prototype.Initialize = function (a, b) {
                this.position.SetV(a);
                this.R.SetM(b)
            };
            f.prototype.SetIdentity = function () {
                this.position.SetZero();
                this.R.SetIdentity()
            };
            f.prototype.Set = function (a) {
                this.position.SetV(a.position);
                this.R.SetM(a.R)
            };
            f.prototype.GetAngle = function () {
                return Math.atan2(this.R.col1.y,
                this.R.col1.x)
            };
            l.b2Vec2 = function () {};
            l.prototype.b2Vec2 = function (a, b) {
                a === void 0 && (a = 0);
                b === void 0 && (b = 0);
                this.x = a;
                this.y = b
            };
            l.prototype.SetZero = function () {
                this.y = this.x = 0
            };
            l.prototype.Set = function (a, b) {
                a === void 0 && (a = 0);
                b === void 0 && (b = 0);
                this.x = a;
                this.y = b
            };
            l.prototype.SetV = function (a) {
                this.x = a.x;
                this.y = a.y
            };
            l.prototype.GetNegative = function () {
                return new l(-this.x, -this.y)
            };
            l.prototype.NegativeSelf = function () {
                this.x = -this.x;
                this.y = -this.y
            };
            l.Make = function (a, b) {
                a === void 0 && (a = 0);
                b === void 0 && (b = 0);
                return new l(a, b)
            };
            l.prototype.Copy = function () {
                return new l(this.x, this.y)
            };
            l.prototype.Add = function (a) {
                this.x += a.x;
                this.y += a.y
            };
            l.prototype.Subtract = function (a) {
                this.x -= a.x;
                this.y -= a.y
            };
            l.prototype.Multiply = function (a) {
                a === void 0 && (a = 0);
                this.x *= a;
                this.y *= a
            };
            l.prototype.MulM = function (a) {
                var b = this.x;
                this.x = a.col1.x * b + a.col2.x * this.y;
                this.y = a.col1.y * b + a.col2.y * this.y
            };
            l.prototype.MulTM = function (a) {
                var b = g.Dot(this, a.col1);
                this.y = g.Dot(this, a.col2);
                this.x = b
            };
            l.prototype.CrossVF = function (a) {
                a === void 0 && (a = 0);
                var b = this.x;
                this.x = a * this.y;
                this.y = -a * b
            };
            l.prototype.CrossFV = function (a) {
                a === void 0 && (a = 0);
                var b = this.x;
                this.x = -a * this.y;
                this.y = a * b
            };
            l.prototype.MinV = function (a) {
                this.x = this.x < a.x ? this.x : a.x;
                this.y = this.y < a.y ? this.y : a.y
            };
            l.prototype.MaxV = function (a) {
                this.x = this.x > a.x ? this.x : a.x;
                this.y = this.y > a.y ? this.y : a.y
            };
            l.prototype.Abs = function () {
                if (this.x < 0) this.x = -this.x;
                if (this.y < 0) this.y = -this.y
            };
            l.prototype.Length = function () {
                return Math.sqrt(this.x * this.x + this.y * this.y)
            };
            l.prototype.LengthSquared = function () {
                return this.x * this.x + this.y * this.y
            };
            l.prototype.Normalize = function () {
                var a = Math.sqrt(this.x * this.x + this.y * this.y);
                if (a < Number.MIN_VALUE) return 0;
                var b = 1 / a;
                this.x *= b;
                this.y *= b;
                return a
            };
            l.prototype.IsValid = function () {
                return g.IsValid(this.x) && g.IsValid(this.y)
            };
            m.b2Vec3 = function () {};
            m.prototype.b2Vec3 = function (a, b, c) {
                a === void 0 && (a = 0);
                b === void 0 && (b = 0);
                c === void 0 && (c = 0);
                this.x = a;
                this.y = b;
                this.z = c
            };
            m.prototype.SetZero = function () {
                this.x = this.y = this.z = 0
            };
            m.prototype.Set = function (a, b, c) {
                a === void 0 && (a = 0);
                b === void 0 && (b = 0);
                c === void 0 && (c = 0);
                this.x = a;
                this.y = b;
                this.z = c
            };
            m.prototype.SetV = function (a) {
                this.x = a.x;
                this.y = a.y;
                this.z = a.z
            };
            m.prototype.GetNegative = function () {
                return new m(-this.x, -this.y, -this.z)
            };
            m.prototype.NegativeSelf = function () {
                this.x = -this.x;
                this.y = -this.y;
                this.z = -this.z
            };
            m.prototype.Copy = function () {
                return new m(this.x, this.y, this.z)
            };
            m.prototype.Add = function (a) {
                this.x += a.x;
                this.y += a.y;
                this.z += a.z
            };
            m.prototype.Subtract = function (a) {
                this.x -= a.x;
                this.y -= a.y;
                this.z -= a.z
            };
            m.prototype.Multiply = function (a) {
                a === void 0 && (a = 0);
                this.x *= a;
                this.y *= a;
                this.z *= a
            }
        })();
        (function () {
            var b = a.Common.Math.b2Math,
                c = a.Common.Math.b2Sweep,
                g = a.Common.Math.b2Transform,
                p = a.Common.Math.b2Vec2,
                f = a.Common.b2Color,
                l = a.Common.b2Settings,
                m = a.Collision.b2AABB,
                d = a.Collision.b2ContactPoint,
                i = a.Collision.b2DynamicTreeBroadPhase,
                A = a.Collision.b2RayCastInput,
                B = a.Collision.b2RayCastOutput,
                F = a.Collision.Shapes.b2CircleShape,
                I = a.Collision.Shapes.b2EdgeShape,
                w = a.Collision.Shapes.b2MassData,
                y = a.Collision.Shapes.b2PolygonShape,
                R = a.Collision.Shapes.b2Shape,
                o = a.Dynamics.b2Body,
                M = a.Dynamics.b2BodyDef,
                D = a.Dynamics.b2ContactFilter,
                N = a.Dynamics.b2ContactImpulse,
                J = a.Dynamics.b2ContactListener,
                K = a.Dynamics.b2ContactManager,
                C = a.Dynamics.b2DebugDraw,
                O = a.Dynamics.b2DestructionListener,
                E = a.Dynamics.b2FilterData,
                H = a.Dynamics.b2Fixture,
                P = a.Dynamics.b2FixtureDef,
                L = a.Dynamics.b2Island,
                j = a.Dynamics.b2TimeStep,
                n = a.Dynamics.b2World,
                q = a.Dynamics.Contacts.b2Contact,
                h = a.Dynamics.Contacts.b2ContactFactory,
                u = a.Dynamics.Contacts.b2ContactSolver,
                G = a.Dynamics.Joints.b2Joint,
                x = a.Dynamics.Joints.b2PulleyJoint;
            o.b2Body = function () {
                this.m_xf = new g;
                this.m_sweep = new c;
                this.m_linearVelocity = new p;
                this.m_force = new p
            };
            o.prototype.connectEdges = function (a, k, j) {
                j === void 0 && (j = 0);
                var h = Math.atan2(k.GetDirectionVector().y, k.GetDirectionVector().x),
                    j = b.MulFV(Math.tan((h - j) * 0.5), k.GetDirectionVector()),
                    j = b.SubtractVV(j, k.GetNormalVector()),
                    j = b.MulFV(l.b2_toiSlop, j),
                    j = b.AddVV(j, k.GetVertex1()),
                    n = b.AddVV(a.GetDirectionVector(), k.GetDirectionVector());
                n.Normalize();
                var c = b.Dot(a.GetDirectionVector(), k.GetNormalVector()) > 0;
                a.SetNextEdge(k, j, n, c);
                k.SetPrevEdge(a, j, n, c);
                return h
            };
            o.prototype.CreateFixture = function (a) {
                if (this.m_world.IsLocked() == !0) return null;
                var k = new H;
                k.Create(this, this.m_xf, a);
                this.m_flags & o.e_activeFlag && k.CreateProxy(this.m_world.m_contactManager.m_broadPhase, this.m_xf);
                k.m_next = this.m_fixtureList;
                this.m_fixtureList = k;
                ++this.m_fixtureCount;
                k.m_body = this;
                k.m_density > 0 && this.ResetMassData();
                this.m_world.m_flags |= n.e_newFixture;
                return k
            };
            o.prototype.CreateFixture2 = function (a, k) {
                k === void 0 && (k = 0);
                var b = new P;
                b.shape = a;
                b.density = k;
                return this.CreateFixture(b)
            };
            o.prototype.DestroyFixture = function (a) {
                if (this.m_world.IsLocked() != !0) {
                    for (var b = this.m_fixtureList, j = null; b != null;) {
                        if (b == a) {
                            j ? j.m_next = a.m_next : this.m_fixtureList = a.m_next;
                            break
                        }
                        j = b;
                        b = b.m_next
                    }
                    for (b = this.m_contactList; b;) {
                        var j = b.contact,
                            b = b.next,
                            h = j.GetFixtureA(),
                            n = j.GetFixtureB();
                        (a == h || a == n) && this.m_world.m_contactManager.Destroy(j)
                    }
                    this.m_flags & o.e_activeFlag && a.DestroyProxy(this.m_world.m_contactManager.m_broadPhase);
                    a.Destroy();
                    a.m_body = null;
                    a.m_next = null;
                    --this.m_fixtureCount;
                    this.ResetMassData()
                }
            };
            o.prototype.SetPositionAndAngle = function (a, b) {
                b === void 0 && (b = 0);
                var j;
                if (this.m_world.IsLocked() != !0) {
                    this.m_xf.R.Set(b);
                    this.m_xf.position.SetV(a);
                    j = this.m_xf.R;
                    var h = this.m_sweep.localCenter;
                    this.m_sweep.c.x = j.col1.x * h.x + j.col2.x * h.y;
                    this.m_sweep.c.y = j.col1.y * h.x + j.col2.y * h.y;
                    this.m_sweep.c.x += this.m_xf.position.x;
                    this.m_sweep.c.y += this.m_xf.position.y;
                    this.m_sweep.c0.SetV(this.m_sweep.c);
                    this.m_sweep.a0 = this.m_sweep.a = b;
                    h = this.m_world.m_contactManager.m_broadPhase;
                    for (j = this.m_fixtureList; j; j = j.m_next) j.Synchronize(h, this.m_xf, this.m_xf);
                    this.m_world.m_contactManager.FindNewContacts()
                }
            };
            o.prototype.SetTransform = function (a) {
                this.SetPositionAndAngle(a.position, a.GetAngle())
            };
            o.prototype.GetTransform = function () {
                return this.m_xf
            };
            o.prototype.GetPosition = function () {
                return this.m_xf.position
            };
            o.prototype.SetPosition = function (a) {
                this.SetPositionAndAngle(a, this.GetAngle())
            };
            o.prototype.GetAngle = function () {
                return this.m_sweep.a
            };
            o.prototype.SetAngle = function (a) {
                a === void 0 && (a = 0);
                this.SetPositionAndAngle(this.GetPosition(), a)
            };
            o.prototype.GetWorldCenter = function () {
                return this.m_sweep.c
            };
            o.prototype.GetLocalCenter = function () {
                return this.m_sweep.localCenter
            };
            o.prototype.SetLinearVelocity = function (a) {
                this.m_type != o.b2_staticBody && this.m_linearVelocity.SetV(a)
            };
            o.prototype.GetLinearVelocity = function () {
                return this.m_linearVelocity
            };
            o.prototype.SetAngularVelocity = function (a) {
                a === void 0 && (a = 0);
                if (this.m_type != o.b2_staticBody) this.m_angularVelocity = a
            };
            o.prototype.GetAngularVelocity = function () {
                return this.m_angularVelocity
            };
            o.prototype.GetDefinition = function () {
                var a = new M;
                a.type = this.GetType();
                a.allowSleep = (this.m_flags & o.e_allowSleepFlag) == o.e_allowSleepFlag;
                a.angle = this.GetAngle();
                a.angularDamping = this.m_angularDamping;
                a.angularVelocity = this.m_angularVelocity;
                a.fixedRotation = (this.m_flags & o.e_fixedRotationFlag) == o.e_fixedRotationFlag;
                a.bullet = (this.m_flags & o.e_bulletFlag) == o.e_bulletFlag;
                a.awake = (this.m_flags & o.e_awakeFlag) == o.e_awakeFlag;
                a.linearDamping = this.m_linearDamping;
                a.linearVelocity.SetV(this.GetLinearVelocity());
                a.position = this.GetPosition();
                a.userData = this.GetUserData();
                return a
            };
            o.prototype.ApplyForce = function (a, b) {
                this.m_type == o.b2_dynamicBody && (this.IsAwake() == !1 && this.SetAwake(!0), this.m_force.x += a.x, this.m_force.y += a.y, this.m_torque += (b.x - this.m_sweep.c.x) * a.y - (b.y - this.m_sweep.c.y) * a.x)
            };
            o.prototype.ApplyTorque = function (a) {
                a === void 0 && (a = 0);
                this.m_type == o.b2_dynamicBody && (this.IsAwake() == !1 && this.SetAwake(!0), this.m_torque += a)
            };
            o.prototype.ApplyImpulse = function (a, b) {
                this.m_type == o.b2_dynamicBody && (this.IsAwake() == !1 && this.SetAwake(!0), this.m_linearVelocity.x += this.m_invMass * a.x, this.m_linearVelocity.y += this.m_invMass * a.y, this.m_angularVelocity += this.m_invI * ((b.x - this.m_sweep.c.x) * a.y - (b.y - this.m_sweep.c.y) * a.x))
            };
            o.prototype.Split = function (a) {
                for (var k = this.GetLinearVelocity().Copy(), j = this.GetAngularVelocity(), h = this.GetWorldCenter(), n = this.m_world.CreateBody(this.GetDefinition()), c, q = this.m_fixtureList; q;) if (a(q)) {
                    var u = q.m_next;
                    c ? c.m_next = u : this.m_fixtureList = u;
                    this.m_fixtureCount--;
                    q.m_next = n.m_fixtureList;
                    n.m_fixtureList = q;
                    n.m_fixtureCount++;
                    q.m_body = n;
                    q = u
                } else c = q, q = q.m_next;
                this.ResetMassData();
                n.ResetMassData();
                c = this.GetWorldCenter();
                a = n.GetWorldCenter();
                c = b.AddVV(k, b.CrossFV(j, b.SubtractVV(c, h)));
                k = b.AddVV(k, b.CrossFV(j, b.SubtractVV(a, h)));
                this.SetLinearVelocity(c);
                n.SetLinearVelocity(k);
                this.SetAngularVelocity(j);
                n.SetAngularVelocity(j);
                this.SynchronizeFixtures();
                n.SynchronizeFixtures();
                return n
            };
            o.prototype.Merge = function (a) {
                var b;
                for (b = a.m_fixtureList; b;) {
                    var j = b.m_next;
                    a.m_fixtureCount--;
                    b.m_next = this.m_fixtureList;
                    this.m_fixtureList = b;
                    this.m_fixtureCount++;
                    b.m_body = h;
                    b = j
                }
                n.m_fixtureCount = 0;
                var n = this,
                    h = a;
                n.GetWorldCenter();
                h.GetWorldCenter();
                n.GetLinearVelocity().Copy();
                h.GetLinearVelocity().Copy();
                n.GetAngularVelocity();
                h.GetAngularVelocity();
                n.ResetMassData();
                this.SynchronizeFixtures()
            };
            o.prototype.GetMass = function () {
                return this.m_mass
            };
            o.prototype.GetInertia = function () {
                return this.m_I
            };
            o.prototype.GetMassData = function (a) {
                a.mass = this.m_mass;
                a.I = this.m_I;
                a.center.SetV(this.m_sweep.localCenter)
            };
            o.prototype.SetMassData = function (a) {
                l.b2Assert(this.m_world.IsLocked() == !1);
                if (this.m_world.IsLocked() != !0 && this.m_type == o.b2_dynamicBody) {
                    this.m_invI = this.m_I = this.m_invMass = 0;
                    this.m_mass = a.mass;
                    if (this.m_mass <= 0) this.m_mass = 1;
                    this.m_invMass = 1 / this.m_mass;
                    if (a.I > 0 && (this.m_flags & o.e_fixedRotationFlag) == 0) this.m_I = a.I - this.m_mass * (a.center.x * a.center.x + a.center.y * a.center.y), this.m_invI = 1 / this.m_I;
                    var k = this.m_sweep.c.Copy();
                    this.m_sweep.localCenter.SetV(a.center);
                    this.m_sweep.c0.SetV(b.MulX(this.m_xf, this.m_sweep.localCenter));
                    this.m_sweep.c.SetV(this.m_sweep.c0);
                    this.m_linearVelocity.x += this.m_angularVelocity * -(this.m_sweep.c.y - k.y);
                    this.m_linearVelocity.y += this.m_angularVelocity * +(this.m_sweep.c.x - k.x)
                }
            };
            o.prototype.ResetMassData = function () {
                this.m_invI = this.m_I = this.m_invMass = this.m_mass = 0;
                this.m_sweep.localCenter.SetZero();
                if (!(this.m_type == o.b2_staticBody || this.m_type == o.b2_kinematicBody)) {
                    for (var a = p.Make(0, 0),
                    k = this.m_fixtureList; k; k = k.m_next) if (k.m_density != 0) {
                        var j = k.GetMassData();
                        this.m_mass += j.mass;
                        a.x += j.center.x * j.mass;
                        a.y += j.center.y * j.mass;
                        this.m_I += j.I
                    }
                    this.m_mass > 0 ? (this.m_invMass = 1 / this.m_mass, a.x *= this.m_invMass, a.y *= this.m_invMass) : this.m_invMass = this.m_mass = 1;
                    this.m_I > 0 && (this.m_flags & o.e_fixedRotationFlag) == 0 ? (this.m_I -= this.m_mass * (a.x * a.x + a.y * a.y), this.m_I *= this.m_inertiaScale, l.b2Assert(this.m_I > 0), this.m_invI = 1 / this.m_I) : this.m_invI = this.m_I = 0;
                    k = this.m_sweep.c.Copy();
                    this.m_sweep.localCenter.SetV(a);
                    this.m_sweep.c0.SetV(b.MulX(this.m_xf, this.m_sweep.localCenter));
                    this.m_sweep.c.SetV(this.m_sweep.c0);
                    this.m_linearVelocity.x += this.m_angularVelocity * -(this.m_sweep.c.y - k.y);
                    this.m_linearVelocity.y += this.m_angularVelocity * +(this.m_sweep.c.x - k.x)
                }
            };
            o.prototype.GetWorldPoint = function (a) {
                var b = this.m_xf.R,
                    a = new p(b.col1.x * a.x + b.col2.x * a.y, b.col1.y * a.x + b.col2.y * a.y);
                a.x += this.m_xf.position.x;
                a.y += this.m_xf.position.y;
                return a
            };
            o.prototype.GetWorldVector = function (a) {
                return b.MulMV(this.m_xf.R, a)
            };
            o.prototype.GetLocalPoint = function (a) {
                return b.MulXT(this.m_xf, a)
            };
            o.prototype.GetLocalVector = function (a) {
                return b.MulTMV(this.m_xf.R, a)
            };
            o.prototype.GetLinearVelocityFromWorldPoint = function (a) {
                return new p(this.m_linearVelocity.x - this.m_angularVelocity * (a.y - this.m_sweep.c.y), this.m_linearVelocity.y + this.m_angularVelocity * (a.x - this.m_sweep.c.x))
            };
            o.prototype.GetLinearVelocityFromLocalPoint = function (a) {
                var b = this.m_xf.R,
                    a = new p(b.col1.x * a.x + b.col2.x * a.y, b.col1.y * a.x + b.col2.y * a.y);
                a.x += this.m_xf.position.x;
                a.y += this.m_xf.position.y;
                return new p(this.m_linearVelocity.x - this.m_angularVelocity * (a.y - this.m_sweep.c.y), this.m_linearVelocity.y + this.m_angularVelocity * (a.x - this.m_sweep.c.x))
            };
            o.prototype.GetLinearDamping = function () {
                return this.m_linearDamping
            };
            o.prototype.SetLinearDamping = function (a) {
                a === void 0 && (a = 0);
                this.m_linearDamping = a
            };
            o.prototype.GetAngularDamping = function () {
                return this.m_angularDamping
            };
            o.prototype.SetAngularDamping = function (a) {
                a === void 0 && (a = 0);
                this.m_angularDamping = a
            };
            o.prototype.SetType = function (a) {
                a === void 0 && (a = 0);
                if (this.m_type != a) {
                    this.m_type = a;
                    this.ResetMassData();
                    if (this.m_type == o.b2_staticBody) this.m_linearVelocity.SetZero(), this.m_angularVelocity = 0;
                    this.SetAwake(!0);
                    this.m_force.SetZero();
                    this.m_torque = 0;
                    for (a = this.m_contactList; a; a = a.next) a.contact.FlagForFiltering()
                }
            };
            o.prototype.GetType = function () {
                return this.m_type
            };
            o.prototype.SetBullet = function (a) {
                a ? this.m_flags |= o.e_bulletFlag : this.m_flags &= ~o.e_bulletFlag
            };
            o.prototype.IsBullet = function () {
                return (this.m_flags & o.e_bulletFlag) == o.e_bulletFlag
            };
            o.prototype.SetSleepingAllowed = function (a) {
                a ? this.m_flags |= o.e_allowSleepFlag : (this.m_flags &= ~o.e_allowSleepFlag, this.SetAwake(!0))
            };
            o.prototype.SetAwake = function (a) {
                a ? (this.m_flags |= o.e_awakeFlag, this.m_sleepTime = 0) : (this.m_flags &= ~o.e_awakeFlag, this.m_sleepTime = 0, this.m_linearVelocity.SetZero(), this.m_angularVelocity = 0, this.m_force.SetZero(), this.m_torque = 0)
            };
            o.prototype.IsAwake = function () {
                return (this.m_flags & o.e_awakeFlag) == o.e_awakeFlag
            };
            o.prototype.SetFixedRotation = function (a) {
                a ? this.m_flags |= o.e_fixedRotationFlag : this.m_flags &= ~o.e_fixedRotationFlag;
                this.ResetMassData()
            };
            o.prototype.IsFixedRotation = function () {
                return (this.m_flags & o.e_fixedRotationFlag) == o.e_fixedRotationFlag
            };
            o.prototype.SetActive = function (a) {
                if (a != this.IsActive()) {
                    var b;
                    if (a) {
                        this.m_flags |= o.e_activeFlag;
                        a = this.m_world.m_contactManager.m_broadPhase;
                        for (b = this.m_fixtureList; b; b = b.m_next) b.CreateProxy(a, this.m_xf)
                    } else {
                        this.m_flags &= ~o.e_activeFlag;
                        a = this.m_world.m_contactManager.m_broadPhase;
                        for (b = this.m_fixtureList; b; b = b.m_next) b.DestroyProxy(a);
                        for (a = this.m_contactList; a;) b = a, a = a.next, this.m_world.m_contactManager.Destroy(b.contact);
                        this.m_contactList = null
                    }
                }
            };
            o.prototype.IsActive = function () {
                return (this.m_flags & o.e_activeFlag) == o.e_activeFlag
            };
            o.prototype.IsSleepingAllowed = function () {
                return (this.m_flags & o.e_allowSleepFlag) == o.e_allowSleepFlag
            };
            o.prototype.GetFixtureList = function () {
                return this.m_fixtureList
            };
            o.prototype.GetJointList = function () {
                return this.m_jointList
            };
            o.prototype.GetControllerList = function () {
                return this.m_controllerList
            };
            o.prototype.GetContactList = function () {
                return this.m_contactList
            };
            o.prototype.GetNext = function () {
                return this.m_next
            };
            o.prototype.GetUserData = function () {
                return this.m_userData
            };
            o.prototype.SetUserData = function (a) {
                this.m_userData = a
            };
            o.prototype.GetWorld = function () {
                return this.m_world
            };
            o.prototype.b2Body = function (a, b) {
                this.m_flags = 0;
                a.bullet && (this.m_flags |= o.e_bulletFlag);
                a.fixedRotation && (this.m_flags |= o.e_fixedRotationFlag);
                a.allowSleep && (this.m_flags |= o.e_allowSleepFlag);
                a.awake && (this.m_flags |= o.e_awakeFlag);
                a.active && (this.m_flags |= o.e_activeFlag);
                this.m_world = b;
                this.m_xf.position.SetV(a.position);
                this.m_xf.R.Set(a.angle);
                this.m_sweep.localCenter.SetZero();
                this.m_sweep.t0 = 1;
                this.m_sweep.a0 = this.m_sweep.a = a.angle;
                var j = this.m_xf.R,
                    n = this.m_sweep.localCenter;
                this.m_sweep.c.x = j.col1.x * n.x + j.col2.x * n.y;
                this.m_sweep.c.y = j.col1.y * n.x + j.col2.y * n.y;
                this.m_sweep.c.x += this.m_xf.position.x;
                this.m_sweep.c.y += this.m_xf.position.y;
                this.m_sweep.c0.SetV(this.m_sweep.c);
                this.m_contactList = this.m_controllerList = this.m_jointList = null;
                this.m_controllerCount = 0;
                this.m_next = this.m_prev = null;
                this.m_linearVelocity.SetV(a.linearVelocity);
                this.m_angularVelocity = a.angularVelocity;
                this.m_linearDamping = a.linearDamping;
                this.m_angularDamping = a.angularDamping;
                this.m_force.Set(0, 0);
                this.m_sleepTime = this.m_torque = 0;
                this.m_type = a.type;
                this.m_invMass = this.m_type == o.b2_dynamicBody ? this.m_mass = 1 : this.m_mass = 0;
                this.m_invI = this.m_I = 0;
                this.m_inertiaScale = a.inertiaScale;
                this.m_userData = a.userData;
                this.m_fixtureList = null;
                this.m_fixtureCount = 0
            };
            o.prototype.SynchronizeFixtures = function () {
                var a = o.s_xf1;
                a.R.Set(this.m_sweep.a0);
                var b = a.R,
                    j = this.m_sweep.localCenter;
                a.position.x = this.m_sweep.c0.x - (b.col1.x * j.x + b.col2.x * j.y);
                a.position.y = this.m_sweep.c0.y - (b.col1.y * j.x + b.col2.y * j.y);
                j = this.m_world.m_contactManager.m_broadPhase;
                for (b = this.m_fixtureList; b; b = b.m_next) b.Synchronize(j, a, this.m_xf)
            };
            o.prototype.SynchronizeTransform = function () {
                this.m_xf.R.Set(this.m_sweep.a);
                var a = this.m_xf.R,
                    b = this.m_sweep.localCenter;
                this.m_xf.position.x = this.m_sweep.c.x - (a.col1.x * b.x + a.col2.x * b.y);
                this.m_xf.position.y = this.m_sweep.c.y - (a.col1.y * b.x + a.col2.y * b.y)
            };
            o.prototype.ShouldCollide = function (a) {
                if (this.m_type != o.b2_dynamicBody && a.m_type != o.b2_dynamicBody) return !1;
                for (var b = this.m_jointList; b; b = b.next) if (b.other == a && b.joint.m_collideConnected == !1) return !1;
                return !0
            };
            o.prototype.Advance = function (a) {
                a === void 0 && (a = 0);
                this.m_sweep.Advance(a);
                this.m_sweep.c.SetV(this.m_sweep.c0);
                this.m_sweep.a = this.m_sweep.a0;
                this.SynchronizeTransform()
            };
            a.postDefs.push(function () {
                a.Dynamics.b2Body.s_xf1 = new g;
                a.Dynamics.b2Body.e_islandFlag = 1;
                a.Dynamics.b2Body.e_awakeFlag = 2;
                a.Dynamics.b2Body.e_allowSleepFlag = 4;
                a.Dynamics.b2Body.e_bulletFlag = 8;
                a.Dynamics.b2Body.e_fixedRotationFlag = 16;
                a.Dynamics.b2Body.e_activeFlag = 32;
                a.Dynamics.b2Body.b2_staticBody = 0;
                a.Dynamics.b2Body.b2_kinematicBody = 1;
                a.Dynamics.b2Body.b2_dynamicBody = 2
            });
            M.b2BodyDef = function () {
                this.position = new p;
                this.linearVelocity = new p
            };
            M.prototype.b2BodyDef = function () {
                this.userData = null;
                this.position.Set(0, 0);
                this.angle = 0;
                this.linearVelocity.Set(0,
                0);
                this.angularDamping = this.linearDamping = this.angularVelocity = 0;
                this.awake = this.allowSleep = !0;
                this.bullet = this.fixedRotation = !1;
                this.type = o.b2_staticBody;
                this.active = !0;
                this.inertiaScale = 1
            };
            D.b2ContactFilter = function () {};
            D.prototype.ShouldCollide = function (a, b) {
                var j = a.GetFilterData(),
                    n = b.GetFilterData();
                if (j.groupIndex == n.groupIndex && j.groupIndex != 0) return j.groupIndex > 0;
                return (j.maskBits & n.categoryBits) != 0 && (j.categoryBits & n.maskBits) != 0
            };
            D.prototype.RayCollide = function (a, b) {
                if (!a) return !0;
                return this.ShouldCollide(a instanceof
                H ? a : null, b)
            };
            a.postDefs.push(function () {
                a.Dynamics.b2ContactFilter.b2_defaultFilter = new D
            });
            N.b2ContactImpulse = function () {
                this.normalImpulses = new r(l.b2_maxManifoldPoints);
                this.tangentImpulses = new r(l.b2_maxManifoldPoints)
            };
            J.b2ContactListener = function () {};
            J.prototype.BeginContact = function () {};
            J.prototype.EndContact = function () {};
            J.prototype.PreSolve = function () {};
            J.prototype.PostSolve = function () {};
            a.postDefs.push(function () {
                a.Dynamics.b2ContactListener.b2_defaultListener = new J
            });
            K.b2ContactManager = function () {};
            K.prototype.b2ContactManager = function () {
                this.m_world = null;
                this.m_contactCount = 0;
                this.m_contactFilter = D.b2_defaultFilter;
                this.m_contactListener = J.b2_defaultListener;
                this.m_contactFactory = new h(this.m_allocator);
                this.m_broadPhase = new i
            };
            K.prototype.AddPair = function (a, b) {
                var j = a instanceof H ? a : null,
                    n = b instanceof H ? b : null,
                    h = j.GetBody(),
                    c = n.GetBody();
                if (h != c) {
                    for (var q = c.GetContactList(); q;) {
                        if (q.other == h) {
                            var u = q.contact.GetFixtureA(),
                                x = q.contact.GetFixtureB();
                            if (u == j && x == n) return;
                            if (u == n && x == j) return
                        }
                        q = q.next
                    }
                    if (c.ShouldCollide(h) != !1 && this.m_contactFilter.ShouldCollide(j, n) != !1) {
                        q = this.m_contactFactory.Create(j, n);
                        j = q.GetFixtureA();
                        n = q.GetFixtureB();
                        h = j.m_body;
                        c = n.m_body;
                        q.m_prev = null;
                        q.m_next = this.m_world.m_contactList;
                        if (this.m_world.m_contactList != null) this.m_world.m_contactList.m_prev = q;
                        this.m_world.m_contactList = q;
                        q.m_nodeA.contact = q;
                        q.m_nodeA.other = c;
                        q.m_nodeA.prev = null;
                        q.m_nodeA.next = h.m_contactList;
                        if (h.m_contactList != null) h.m_contactList.prev = q.m_nodeA;
                        h.m_contactList = q.m_nodeA;
                        q.m_nodeB.contact = q;
                        q.m_nodeB.other = h;
                        q.m_nodeB.prev = null;
                        q.m_nodeB.next = c.m_contactList;
                        if (c.m_contactList != null) c.m_contactList.prev = q.m_nodeB;
                        c.m_contactList = q.m_nodeB;
                        ++this.m_world.m_contactCount
                    }
                }
            };
            K.prototype.FindNewContacts = function () {
                this.m_broadPhase.UpdatePairs(a.generateCallback(this, this.AddPair))
            };
            K.prototype.Destroy = function (a) {
                var b = a.GetFixtureA(),
                    j = a.GetFixtureB(),
                    b = b.GetBody(),
                    j = j.GetBody();
                a.IsTouching() && this.m_contactListener.EndContact(a);
                if (a.m_prev) a.m_prev.m_next = a.m_next;
                if (a.m_next) a.m_next.m_prev = a.m_prev;
                if (a == this.m_world.m_contactList) this.m_world.m_contactList = a.m_next;
                if (a.m_nodeA.prev) a.m_nodeA.prev.next = a.m_nodeA.next;
                if (a.m_nodeA.next) a.m_nodeA.next.prev = a.m_nodeA.prev;
                if (a.m_nodeA == b.m_contactList) b.m_contactList = a.m_nodeA.next;
                if (a.m_nodeB.prev) a.m_nodeB.prev.next = a.m_nodeB.next;
                if (a.m_nodeB.next) a.m_nodeB.next.prev = a.m_nodeB.prev;
                if (a.m_nodeB == j.m_contactList) j.m_contactList = a.m_nodeB.next;
                this.m_contactFactory.Destroy(a);
                --this.m_contactCount
            };
            K.prototype.Collide = function () {
                for (var a = this.m_world.m_contactList; a;) {
                    var b = a.GetFixtureA(),
                        j = a.GetFixtureB(),
                        n = b.GetBody(),
                        h = j.GetBody();
                    if (n.IsAwake() == !1 && h.IsAwake() == !1) a = a.GetNext();
                    else {
                        if (a.m_flags & q.e_filterFlag) {
                            if (h.ShouldCollide(n) == !1) {
                                b = a;
                                a = b.GetNext();
                                this.Destroy(b);
                                continue
                            }
                            if (this.m_contactFilter.ShouldCollide(b, j) == !1) {
                                b = a;
                                a = b.GetNext();
                                this.Destroy(b);
                                continue
                            }
                            a.m_flags &= ~q.e_filterFlag
                        }
                        this.m_broadPhase.TestOverlap(b.m_proxy, j.m_proxy) == !1 ? (b = a, a = b.GetNext(), this.Destroy(b)) : (a.Update(this.m_contactListener), a = a.GetNext())
                    }
                }
            };
            a.postDefs.push(function () {
                a.Dynamics.b2ContactManager.s_evalCP = new d
            });
            C.b2DebugDraw = function () {};
            C.prototype.b2DebugDraw = function () {};
            C.prototype.SetFlags = function () {};
            C.prototype.GetFlags = function () {};
            C.prototype.AppendFlags = function () {};
            C.prototype.ClearFlags = function () {};
            C.prototype.SetSprite = function () {};
            C.prototype.GetSprite = function () {};
            C.prototype.SetDrawScale = function () {};
            C.prototype.GetDrawScale = function () {};
            C.prototype.SetLineThickness = function () {};
            C.prototype.GetLineThickness = function () {};
            C.prototype.SetAlpha = function () {};
            C.prototype.GetAlpha = function () {};
            C.prototype.SetFillAlpha = function () {};
            C.prototype.GetFillAlpha = function () {};
            C.prototype.SetXFormScale = function () {};
            C.prototype.GetXFormScale = function () {};
            C.prototype.DrawPolygon = function () {};
            C.prototype.DrawSolidPolygon = function () {};
            C.prototype.DrawCircle = function () {};
            C.prototype.DrawSolidCircle = function () {};
            C.prototype.DrawSegment = function () {};
            C.prototype.DrawTransform = function () {};
            a.postDefs.push(function () {
                a.Dynamics.b2DebugDraw.e_shapeBit = 1;
                a.Dynamics.b2DebugDraw.e_jointBit = 2;
                a.Dynamics.b2DebugDraw.e_aabbBit = 4;
                a.Dynamics.b2DebugDraw.e_pairBit = 8;
                a.Dynamics.b2DebugDraw.e_centerOfMassBit = 16;
                a.Dynamics.b2DebugDraw.e_controllerBit = 32
            });
            O.b2DestructionListener = function () {};
            O.prototype.SayGoodbyeJoint = function () {};
            O.prototype.SayGoodbyeFixture = function () {};
            E.b2FilterData = function () {
                this.categoryBits = 1;
                this.maskBits = 65535;
                this.groupIndex = 0
            };
            E.prototype.Copy = function () {
                var a = new E;
                a.categoryBits = this.categoryBits;
                a.maskBits = this.maskBits;
                a.groupIndex = this.groupIndex;
                return a
            };
            H.b2Fixture = function () {
                this.m_filter = new E
            };
            H.prototype.GetType = function () {
                return this.m_shape.GetType()
            };
            H.prototype.GetShape = function () {
                return this.m_shape
            };
            H.prototype.SetSensor = function (a) {
                if (this.m_isSensor != a && (this.m_isSensor = a, this.m_body != null)) for (a = this.m_body.GetContactList(); a;) {
                    var b = a.contact,
                        j = b.GetFixtureA(),
                        n = b.GetFixtureB();
                    if (j == this || n == this) b.SetSensor(j.IsSensor() || n.IsSensor());
                    a = a.next
                }
            };
            H.prototype.IsSensor = function () {
                return this.m_isSensor
            };
            H.prototype.SetFilterData = function (a) {
                this.m_filter = a.Copy();
                if (!this.m_body) for (a = this.m_body.GetContactList(); a;) {
                    var b = a.contact,
                        j = b.GetFixtureA(),
                        n = b.GetFixtureB();
                    (j == this || n == this) && b.FlagForFiltering();
                    a = a.next
                }
            };
            H.prototype.GetFilterData = function () {
                return this.m_filter.Copy()
            };
            H.prototype.GetBody = function () {
                return this.m_body
            };
            H.prototype.GetNext = function () {
                return this.m_next
            };
            H.prototype.GetUserData = function () {
                return this.m_userData
            };
            H.prototype.SetUserData = function (a) {
                this.m_userData = a
            };
            H.prototype.TestPoint = function (a) {
                return this.m_shape.TestPoint(this.m_body.GetTransform(), a)
            };
            H.prototype.RayCast = function (a, b) {
                return this.m_shape.RayCast(a, b, this.m_body.GetTransform())
            };
            H.prototype.GetMassData = function (a) {
                a === void 0 && (a = null);
                a == null && (a = new w);
                this.m_shape.ComputeMass(a, this.m_density);
                return a
            };
            H.prototype.SetDensity = function (a) {
                a === void 0 && (a = 0);
                this.m_density = a
            };
            H.prototype.GetDensity = function () {
                return this.m_density
            };
            H.prototype.GetFriction = function () {
                return this.m_friction
            };
            H.prototype.SetFriction = function (a) {
                a === void 0 && (a = 0);
                this.m_friction = a
            };
            H.prototype.GetRestitution = function () {
                return this.m_restitution
            };
            H.prototype.SetRestitution = function (a) {
                a === void 0 && (a = 0);
                this.m_restitution = a
            };
            H.prototype.GetAABB = function () {
                return this.m_aabb
            };
            H.prototype.b2Fixture = function () {
                this.m_aabb = new m;
                this.m_shape = this.m_next = this.m_body = this.m_userData = null;
                this.m_restitution = this.m_friction = this.m_density = 0
            };
            H.prototype.Create = function (a, b, j) {
                this.m_userData = j.userData;
                this.m_friction = j.friction;
                this.m_restitution = j.restitution;
                this.m_body = a;
                this.m_next = null;
                this.m_filter = j.filter.Copy();
                this.m_isSensor = j.isSensor;
                this.m_shape = j.shape.Copy();
                this.m_density = j.density
            };
            H.prototype.Destroy = function () {
                this.m_shape = null
            };
            H.prototype.CreateProxy = function (a, b) {
                this.m_shape.ComputeAABB(this.m_aabb, b);
                this.m_proxy = a.CreateProxy(this.m_aabb, this)
            };
            H.prototype.DestroyProxy = function (a) {
                if (this.m_proxy != null) a.DestroyProxy(this.m_proxy),
                this.m_proxy = null
            };
            H.prototype.Synchronize = function (a, j, n) {
                if (this.m_proxy) {
                    var h = new m,
                        c = new m;
                    this.m_shape.ComputeAABB(h, j);
                    this.m_shape.ComputeAABB(c, n);
                    this.m_aabb.Combine(h, c);
                    j = b.SubtractVV(n.position, j.position);
                    a.MoveProxy(this.m_proxy, this.m_aabb, j)
                }
            };
            P.b2FixtureDef = function () {
                this.filter = new E
            };
            P.prototype.b2FixtureDef = function () {
                this.userData = this.shape = null;
                this.friction = 0.2;
                this.density = this.restitution = 0;
                this.filter.categoryBits = 1;
                this.filter.maskBits = 65535;
                this.filter.groupIndex = 0;
                this.isSensor = !1
            };
            L.b2Island = function () {};
            L.prototype.b2Island = function () {
                this.m_bodies = new s;
                this.m_contacts = new s;
                this.m_joints = new s
            };
            L.prototype.Initialize = function (a, b, j, n, h, c) {
                a === void 0 && (a = 0);
                b === void 0 && (b = 0);
                j === void 0 && (j = 0);
                var q = 0;
                this.m_bodyCapacity = a;
                this.m_contactCapacity = b;
                this.m_jointCapacity = j;
                this.m_jointCount = this.m_contactCount = this.m_bodyCount = 0;
                this.m_allocator = n;
                this.m_listener = h;
                this.m_contactSolver = c;
                for (q = this.m_bodies.length; q < a; q++) this.m_bodies[q] = null;
                for (q = this.m_contacts.length; q < b; q++) this.m_contacts[q] = null;
                for (q = this.m_joints.length; q < j; q++) this.m_joints[q] = null
            };
            L.prototype.Clear = function () {
                this.m_jointCount = this.m_contactCount = this.m_bodyCount = 0
            };
            L.prototype.Solve = function (a, j, n) {
                for (var h = 0, c = 0, q, h = 0; h < this.m_bodyCount; ++h) c = this.m_bodies[h], c.GetType() == o.b2_dynamicBody && (c.m_linearVelocity.x += a.dt * (j.x + c.m_invMass * c.m_force.x), c.m_linearVelocity.y += a.dt * (j.y + c.m_invMass * c.m_force.y), c.m_angularVelocity += a.dt * c.m_invI * c.m_torque, c.m_linearVelocity.Multiply(b.Clamp(1 - a.dt * c.m_linearDamping, 0, 1)), c.m_angularVelocity *= b.Clamp(1 - a.dt * c.m_angularDamping, 0, 1));
                this.m_contactSolver.Initialize(a, this.m_contacts, this.m_contactCount, this.m_allocator);
                j = this.m_contactSolver;
                j.InitVelocityConstraints(a);
                for (h = 0; h < this.m_jointCount; ++h) q = this.m_joints[h], q.InitVelocityConstraints(a);
                for (h = 0; h < a.velocityIterations; ++h) {
                    for (c = 0; c < this.m_jointCount; ++c) q = this.m_joints[c], q.SolveVelocityConstraints(a);
                    j.SolveVelocityConstraints()
                }
                for (h = 0; h < this.m_jointCount; ++h) q = this.m_joints[h],
                q.FinalizeVelocityConstraints();
                j.FinalizeVelocityConstraints();
                for (h = 0; h < this.m_bodyCount; ++h) if (c = this.m_bodies[h], c.GetType() != o.b2_staticBody) {
                    var u = a.dt * c.m_linearVelocity.x,
                        x = a.dt * c.m_linearVelocity.y;
                    u * u + x * x > l.b2_maxTranslationSquared && (c.m_linearVelocity.Normalize(), c.m_linearVelocity.x *= l.b2_maxTranslation * a.inv_dt, c.m_linearVelocity.y *= l.b2_maxTranslation * a.inv_dt);
                    u = a.dt * c.m_angularVelocity;
                    if (u * u > l.b2_maxRotationSquared) c.m_angularVelocity = c.m_angularVelocity < 0 ? -l.b2_maxRotation * a.inv_dt : l.b2_maxRotation * a.inv_dt;
                    c.m_sweep.c0.SetV(c.m_sweep.c);
                    c.m_sweep.a0 = c.m_sweep.a;
                    c.m_sweep.c.x += a.dt * c.m_linearVelocity.x;
                    c.m_sweep.c.y += a.dt * c.m_linearVelocity.y;
                    c.m_sweep.a += a.dt * c.m_angularVelocity;
                    c.SynchronizeTransform()
                }
                for (h = 0; h < a.positionIterations; ++h) {
                    u = j.SolvePositionConstraints(l.b2_contactBaumgarte);
                    x = !0;
                    for (c = 0; c < this.m_jointCount; ++c) q = this.m_joints[c], q = q.SolvePositionConstraints(l.b2_contactBaumgarte), x = x && q;
                    if (u && x) break
                }
                this.Report(j.m_constraints);
                if (n) {
                    n = Number.MAX_VALUE;
                    j = l.b2_linearSleepTolerance * l.b2_linearSleepTolerance;
                    u = l.b2_angularSleepTolerance * l.b2_angularSleepTolerance;
                    for (h = 0; h < this.m_bodyCount; ++h) if (c = this.m_bodies[h], c.GetType() != o.b2_staticBody) {
                        if ((c.m_flags & o.e_allowSleepFlag) == 0) n = c.m_sleepTime = 0;
                        (c.m_flags & o.e_allowSleepFlag) == 0 || c.m_angularVelocity * c.m_angularVelocity > u || b.Dot(c.m_linearVelocity, c.m_linearVelocity) > j ? n = c.m_sleepTime = 0 : (c.m_sleepTime += a.dt, n = b.Min(n, c.m_sleepTime))
                    }
                    if (n >= l.b2_timeToSleep) for (h = 0; h < this.m_bodyCount; ++h) c = this.m_bodies[h], c.SetAwake(!1)
                }
            };
            L.prototype.SolveTOI = function (a) {
                var b = 0,
                    j = 0;
                this.m_contactSolver.Initialize(a, this.m_contacts, this.m_contactCount, this.m_allocator);
                for (var h = this.m_contactSolver, b = 0; b < this.m_jointCount; ++b) this.m_joints[b].InitVelocityConstraints(a);
                for (b = 0; b < a.velocityIterations; ++b) {
                    h.SolveVelocityConstraints();
                    for (j = 0; j < this.m_jointCount; ++j) this.m_joints[j].SolveVelocityConstraints(a)
                }
                for (b = 0; b < this.m_bodyCount; ++b) if (j = this.m_bodies[b], j.GetType() != o.b2_staticBody) {
                    var n = a.dt * j.m_linearVelocity.x,
                        c = a.dt * j.m_linearVelocity.y;
                    n * n + c * c > l.b2_maxTranslationSquared && (j.m_linearVelocity.Normalize(), j.m_linearVelocity.x *= l.b2_maxTranslation * a.inv_dt, j.m_linearVelocity.y *= l.b2_maxTranslation * a.inv_dt);
                    n = a.dt * j.m_angularVelocity;
                    if (n * n > l.b2_maxRotationSquared) j.m_angularVelocity = j.m_angularVelocity < 0 ? -l.b2_maxRotation * a.inv_dt : l.b2_maxRotation * a.inv_dt;
                    j.m_sweep.c0.SetV(j.m_sweep.c);
                    j.m_sweep.a0 = j.m_sweep.a;
                    j.m_sweep.c.x += a.dt * j.m_linearVelocity.x;
                    j.m_sweep.c.y += a.dt * j.m_linearVelocity.y;
                    j.m_sweep.a += a.dt * j.m_angularVelocity;
                    j.SynchronizeTransform()
                }
                for (b = 0; b < a.positionIterations; ++b) {
                    n = h.SolvePositionConstraints(0.75);
                    c = !0;
                    for (j = 0; j < this.m_jointCount; ++j) var q = this.m_joints[j].SolvePositionConstraints(l.b2_contactBaumgarte),
                        c = c && q;
                    if (n && c) break
                }
                this.Report(h.m_constraints)
            };
            L.prototype.Report = function (a) {
                if (this.m_listener != null) for (var b = 0; b < this.m_contactCount; ++b) {
                    for (var j = this.m_contacts[b], h = a[b], n = 0; n < h.pointCount; ++n) L.s_impulse.normalImpulses[n] = h.points[n].normalImpulse, L.s_impulse.tangentImpulses[n] = h.points[n].tangentImpulse;
                    this.m_listener.PostSolve(j,
                    L.s_impulse)
                }
            };
            L.prototype.AddBody = function (a) {
                a.m_islandIndex = this.m_bodyCount;
                this.m_bodies[this.m_bodyCount++] = a
            };
            L.prototype.AddContact = function (a) {
                this.m_contacts[this.m_contactCount++] = a
            };
            L.prototype.AddJoint = function (a) {
                this.m_joints[this.m_jointCount++] = a
            };
            a.postDefs.push(function () {
                a.Dynamics.b2Island.s_impulse = new N
            });
            j.b2TimeStep = function () {};
            j.prototype.Set = function (a) {
                this.dt = a.dt;
                this.inv_dt = a.inv_dt;
                this.positionIterations = a.positionIterations;
                this.velocityIterations = a.velocityIterations;
                this.warmStarting = a.warmStarting
            };
            n.b2World = function () {
                this.s_stack = new s;
                this.m_contactManager = new K;
                this.m_contactSolver = new u;
                this.m_island = new L
            };
            n.prototype.b2World = function (a, b) {
                this.m_controllerList = this.m_jointList = this.m_contactList = this.m_bodyList = this.m_debugDraw = this.m_destructionListener = null;
                this.m_controllerCount = this.m_jointCount = this.m_contactCount = this.m_bodyCount = 0;
                n.m_warmStarting = !0;
                n.m_continuousPhysics = !0;
                this.m_allowSleep = b;
                this.m_gravity = a;
                this.m_inv_dt0 = 0;
                this.m_contactManager.m_world = this;
                this.m_groundBody = this.CreateBody(new M)
            };
            n.prototype.SetDestructionListener = function (a) {
                this.m_destructionListener = a
            };
            n.prototype.SetContactFilter = function (a) {
                this.m_contactManager.m_contactFilter = a
            };
            n.prototype.SetContactListener = function (a) {
                this.m_contactManager.m_contactListener = a
            };
            n.prototype.SetDebugDraw = function (a) {
                this.m_debugDraw = a
            };
            n.prototype.SetBroadPhase = function (a) {
                var b = this.m_contactManager.m_broadPhase;
                this.m_contactManager.m_broadPhase = a;
                for (var j = this.m_bodyList; j; j = j.m_next) for (var h = j.m_fixtureList; h; h = h.m_next) h.m_proxy = a.CreateProxy(b.GetFatAABB(h.m_proxy), h)
            };
            n.prototype.Validate = function () {
                this.m_contactManager.m_broadPhase.Validate()
            };
            n.prototype.GetProxyCount = function () {
                return this.m_contactManager.m_broadPhase.GetProxyCount()
            };
            n.prototype.CreateBody = function (a) {
                if (this.IsLocked() == !0) return null;
                a = new o(a, this);
                a.m_prev = null;
                if (a.m_next = this.m_bodyList) this.m_bodyList.m_prev = a;
                this.m_bodyList = a;
                ++this.m_bodyCount;
                return a
            };
            n.prototype.DestroyBody = function (a) {
                if (this.IsLocked() != !0) {
                    for (var b = a.m_jointList; b;) {
                        var j = b,
                            b = b.next;
                        this.m_destructionListener && this.m_destructionListener.SayGoodbyeJoint(j.joint);
                        this.DestroyJoint(j.joint)
                    }
                    for (b = a.m_controllerList; b;) j = b, b = b.nextController, j.controller.RemoveBody(a);
                    for (b = a.m_contactList; b;) j = b, b = b.next, this.m_contactManager.Destroy(j.contact);
                    a.m_contactList = null;
                    for (b = a.m_fixtureList; b;) j = b, b = b.m_next, this.m_destructionListener && this.m_destructionListener.SayGoodbyeFixture(j), j.DestroyProxy(this.m_contactManager.m_broadPhase),
                    j.Destroy();
                    a.m_fixtureList = null;
                    a.m_fixtureCount = 0;
                    if (a.m_prev) a.m_prev.m_next = a.m_next;
                    if (a.m_next) a.m_next.m_prev = a.m_prev;
                    if (a == this.m_bodyList) this.m_bodyList = a.m_next;
                    --this.m_bodyCount
                }
            };
            n.prototype.CreateJoint = function (a) {
                var b = G.Create(a, null);
                b.m_prev = null;
                if (b.m_next = this.m_jointList) this.m_jointList.m_prev = b;
                this.m_jointList = b;
                ++this.m_jointCount;
                b.m_edgeA.joint = b;
                b.m_edgeA.other = b.m_bodyB;
                b.m_edgeA.prev = null;
                if (b.m_edgeA.next = b.m_bodyA.m_jointList) b.m_bodyA.m_jointList.prev = b.m_edgeA;
                b.m_bodyA.m_jointList = b.m_edgeA;
                b.m_edgeB.joint = b;
                b.m_edgeB.other = b.m_bodyA;
                b.m_edgeB.prev = null;
                if (b.m_edgeB.next = b.m_bodyB.m_jointList) b.m_bodyB.m_jointList.prev = b.m_edgeB;
                b.m_bodyB.m_jointList = b.m_edgeB;
                var j = a.bodyA,
                    h = a.bodyB;
                if (a.collideConnected == !1) for (a = h.GetContactList(); a;) a.other == j && a.contact.FlagForFiltering(), a = a.next;
                return b
            };
            n.prototype.DestroyJoint = function (a) {
                var b = a.m_collideConnected;
                if (a.m_prev) a.m_prev.m_next = a.m_next;
                if (a.m_next) a.m_next.m_prev = a.m_prev;
                if (a == this.m_jointList) this.m_jointList = a.m_next;
                var j = a.m_bodyA,
                    h = a.m_bodyB;
                j.SetAwake(!0);
                h.SetAwake(!0);
                if (a.m_edgeA.prev) a.m_edgeA.prev.next = a.m_edgeA.next;
                if (a.m_edgeA.next) a.m_edgeA.next.prev = a.m_edgeA.prev;
                if (a.m_edgeA == j.m_jointList) j.m_jointList = a.m_edgeA.next;
                a.m_edgeA.prev = null;
                a.m_edgeA.next = null;
                if (a.m_edgeB.prev) a.m_edgeB.prev.next = a.m_edgeB.next;
                if (a.m_edgeB.next) a.m_edgeB.next.prev = a.m_edgeB.prev;
                if (a.m_edgeB == h.m_jointList) h.m_jointList = a.m_edgeB.next;
                a.m_edgeB.prev = null;
                a.m_edgeB.next = null;
                G.Destroy(a, null);
                --this.m_jointCount;
                if (b == !1) for (a = h.GetContactList(); a;) a.other == j && a.contact.FlagForFiltering(), a = a.next
            };
            n.prototype.AddController = function (a) {
                a.m_next = this.m_controllerList;
                a.m_prev = null;
                this.m_controllerList = a;
                a.m_world = this;
                this.m_controllerCount++;
                return a
            };
            n.prototype.RemoveController = function (a) {
                if (a.m_prev) a.m_prev.m_next = a.m_next;
                if (a.m_next) a.m_next.m_prev = a.m_prev;
                if (this.m_controllerList == a) this.m_controllerList = a.m_next;
                this.m_controllerCount--
            };
            n.prototype.CreateController = function (a) {
                if (a.m_world != this) throw Error("Controller can only be a member of one world");
                a.m_next = this.m_controllerList;
                a.m_prev = null;
                if (this.m_controllerList) this.m_controllerList.m_prev = a;
                this.m_controllerList = a;
                ++this.m_controllerCount;
                a.m_world = this;
                return a
            };
            n.prototype.DestroyController = function (a) {
                a.Clear();
                if (a.m_next) a.m_next.m_prev = a.m_prev;
                if (a.m_prev) a.m_prev.m_next = a.m_next;
                if (a == this.m_controllerList) this.m_controllerList = a.m_next;
                --this.m_controllerCount
            };
            n.prototype.SetWarmStarting = function (a) {
                n.m_warmStarting = a
            };
            n.prototype.SetContinuousPhysics = function (a) {
                n.m_continuousPhysics = a
            };
            n.prototype.GetBodyCount = function () {
                return this.m_bodyCount
            };
            n.prototype.GetJointCount = function () {
                return this.m_jointCount
            };
            n.prototype.GetContactCount = function () {
                return this.m_contactCount
            };
            n.prototype.SetGravity = function (a) {
                this.m_gravity = a
            };
            n.prototype.GetGravity = function () {
                return this.m_gravity
            };
            n.prototype.GetGroundBody = function () {
                return this.m_groundBody
            };
            n.prototype.Step = function (a, b, j) {
                a === void 0 && (a = 0);
                b === void 0 && (b = 0);
                j === void 0 && (j = 0);
                this.m_flags & n.e_newFixture && (this.m_contactManager.FindNewContacts(),
                this.m_flags &= ~n.e_newFixture);
                this.m_flags |= n.e_locked;
                var h = n.s_timestep2;
                h.dt = a;
                h.velocityIterations = b;
                h.positionIterations = j;
                h.inv_dt = a > 0 ? 1 / a : 0;
                h.dtRatio = this.m_inv_dt0 * a;
                h.warmStarting = n.m_warmStarting;
                this.m_contactManager.Collide();
                h.dt > 0 && this.Solve(h);
                n.m_continuousPhysics && h.dt > 0 && this.SolveTOI(h);
                if (h.dt > 0) this.m_inv_dt0 = h.inv_dt;
                this.m_flags &= ~n.e_locked
            };
            n.prototype.ClearForces = function () {
                for (var a = this.m_bodyList; a; a = a.m_next) a.m_force.SetZero(), a.m_torque = 0
            };
            n.prototype.DrawDebugData = function () {
                if (this.m_debugDraw != null) {
                    this.m_debugDraw.m_sprite.graphics.clear();
                    var a = this.m_debugDraw.GetFlags(),
                        b, j, h;
                    new p;
                    new p;
                    new p;
                    var c;
                    new m;
                    new m;
                    c = [new p, new p, new p, new p];
                    var q = new f(0, 0, 0);
                    if (a & C.e_shapeBit) for (b = this.m_bodyList; b; b = b.m_next) {
                        c = b.m_xf;
                        for (j = b.GetFixtureList(); j; j = j.m_next) h = j.GetShape(), b.IsActive() == !1 ? q.Set(0.5, 0.5, 0.3) : b.GetType() == o.b2_staticBody ? q.Set(0.5, 0.9, 0.5) : b.GetType() == o.b2_kinematicBody ? q.Set(0.5, 0.5, 0.9) : b.IsAwake() == !1 ? q.Set(0.6, 0.6, 0.6) : q.Set(0.9, 0.7,
                        0.7), this.DrawShape(h, c, q)
                    }
                    if (a & C.e_jointBit) for (b = this.m_jointList; b; b = b.m_next) this.DrawJoint(b);
                    if (a & C.e_controllerBit) for (b = this.m_controllerList; b; b = b.m_next) b.Draw(this.m_debugDraw);
                    if (a & C.e_pairBit) {
                        q.Set(0.3, 0.9, 0.9);
                        for (b = this.m_contactManager.m_contactList; b; b = b.GetNext()) h = b.GetFixtureA(), j = b.GetFixtureB(), h = h.GetAABB().GetCenter(), j = j.GetAABB().GetCenter(), this.m_debugDraw.DrawSegment(h, j, q)
                    }
                    if (a & C.e_aabbBit) {
                        h = this.m_contactManager.m_broadPhase;
                        c = [new p, new p, new p, new p];
                        for (b = this.m_bodyList; b; b = b.GetNext()) if (b.IsActive() != !1) for (j = b.GetFixtureList(); j; j = j.GetNext()) {
                            var u = h.GetFatAABB(j.m_proxy);
                            c[0].Set(u.lowerBound.x, u.lowerBound.y);
                            c[1].Set(u.upperBound.x, u.lowerBound.y);
                            c[2].Set(u.upperBound.x, u.upperBound.y);
                            c[3].Set(u.lowerBound.x, u.upperBound.y);
                            this.m_debugDraw.DrawPolygon(c, 4, q)
                        }
                    }
                    if (a & C.e_centerOfMassBit) for (b = this.m_bodyList; b; b = b.m_next) c = n.s_xf, c.R = b.m_xf.R, c.position = b.GetWorldCenter(), this.m_debugDraw.DrawTransform(c)
                }
            };
            n.prototype.QueryAABB = function (a, b) {
                var j = this.m_contactManager.m_broadPhase;
                j.Query(function (b) {
                    return a(j.GetUserData(b))
                }, b)
            };
            n.prototype.QueryShape = function (a, b, j) {
                j === void 0 && (j = null);
                j == null && (j = new g, j.SetIdentity());
                var h = this.m_contactManager.m_broadPhase,
                    n = new m;
                b.ComputeAABB(n, j);
                h.Query(function (n) {
                    n = h.GetUserData(n) instanceof H ? h.GetUserData(n) : null;
                    if (R.TestOverlap(b, j, n.GetShape(), n.GetBody().GetTransform())) return a(n);
                    return !0
                }, n)
            };
            n.prototype.QueryPoint = function (a, b) {
                var j = this.m_contactManager.m_broadPhase,
                    h = new m;
                h.lowerBound.Set(b.x - l.b2_linearSlop, b.y - l.b2_linearSlop);
                h.upperBound.Set(b.x + l.b2_linearSlop, b.y + l.b2_linearSlop);
                j.Query(function (h) {
                    h = j.GetUserData(h) instanceof H ? j.GetUserData(h) : null;
                    if (h.TestPoint(b)) return a(h);
                    return !0
                }, h)
            };
            n.prototype.RayCast = function (a, b, j) {
                var h = this.m_contactManager.m_broadPhase,
                    n = new B,
                    c = new A(b, j);
                h.RayCast(function (c, q) {
                    var u = h.GetUserData(q),
                        u = u instanceof H ? u : null;
                    if (u.RayCast(n, c)) {
                        var x = n.fraction,
                            d = new p((1 - x) * b.x + x * j.x, (1 - x) * b.y + x * j.y);
                        return a(u, d, n.normal, x)
                    }
                    return c.maxFraction
                }, c)
            };
            n.prototype.RayCastOne = function (a, b) {
                var j;
                this.RayCast(function (a, b, e, h) {
                    h === void 0 && (h = 0);
                    j = a;
                    return h
                }, a, b);
                return j
            };
            n.prototype.RayCastAll = function (a, b) {
                var j = new s;
                this.RayCast(function (a) {
                    j[j.length] = a;
                    return 1
                }, a, b);
                return j
            };
            n.prototype.GetBodyList = function () {
                return this.m_bodyList
            };
            n.prototype.GetJointList = function () {
                return this.m_jointList
            };
            n.prototype.GetContactList = function () {
                return this.m_contactList
            };
            n.prototype.IsLocked = function () {
                return (this.m_flags & n.e_locked) > 0
            };
            n.prototype.Solve = function (a) {
                for (var b,
                j = this.m_controllerList; j; j = j.m_next) j.Step(a);
                j = this.m_island;
                j.Initialize(this.m_bodyCount, this.m_contactCount, this.m_jointCount, null, this.m_contactManager.m_contactListener, this.m_contactSolver);
                for (b = this.m_bodyList; b; b = b.m_next) b.m_flags &= ~o.e_islandFlag;
                for (var h = this.m_contactList; h; h = h.m_next) h.m_flags &= ~q.e_islandFlag;
                for (h = this.m_jointList; h; h = h.m_next) h.m_islandFlag = !1;
                parseInt(this.m_bodyCount);
                for (var h = this.s_stack, n = this.m_bodyList; n; n = n.m_next) if (!(n.m_flags & o.e_islandFlag) && !(n.IsAwake() == !1 || n.IsActive() == !1) && n.GetType() != o.b2_staticBody) {
                    j.Clear();
                    var c = 0;
                    h[c++] = n;
                    for (n.m_flags |= o.e_islandFlag; c > 0;) if (b = h[--c], j.AddBody(b), b.IsAwake() == !1 && b.SetAwake(!0), b.GetType() != o.b2_staticBody) {
                        for (var u, x = b.m_contactList; x; x = x.next) if (!(x.contact.m_flags & q.e_islandFlag) && !(x.contact.IsSensor() == !0 || x.contact.IsEnabled() == !1 || x.contact.IsTouching() == !1)) j.AddContact(x.contact), x.contact.m_flags |= q.e_islandFlag, u = x.other, u.m_flags & o.e_islandFlag || (h[c++] = u, u.m_flags |= o.e_islandFlag);
                        for (b = b.m_jointList; b; b = b.next) if (b.joint.m_islandFlag != !0 && (u = b.other, u.IsActive() != !1)) j.AddJoint(b.joint), b.joint.m_islandFlag = !0, u.m_flags & o.e_islandFlag || (h[c++] = u, u.m_flags |= o.e_islandFlag)
                    }
                    j.Solve(a, this.m_gravity, this.m_allowSleep);
                    for (c = 0; c < j.m_bodyCount; ++c) b = j.m_bodies[c], b.GetType() == o.b2_staticBody && (b.m_flags &= ~o.e_islandFlag)
                }
                for (c = 0; c < h.length; ++c) {
                    if (!h[c]) break;
                    h[c] = null
                }
                for (b = this.m_bodyList; b; b = b.m_next) b.IsAwake() == !1 || b.IsActive() == !1 || b.GetType() != o.b2_staticBody && b.SynchronizeFixtures();
                this.m_contactManager.FindNewContacts()
            };
            n.prototype.SolveTOI = function (a) {
                var b, j, h, c = this.m_island;
                c.Initialize(this.m_bodyCount, l.b2_maxTOIContactsPerIsland, l.b2_maxTOIJointsPerIsland, null, this.m_contactManager.m_contactListener, this.m_contactSolver);
                var u = n.s_queue;
                for (b = this.m_bodyList; b; b = b.m_next) b.m_flags &= ~o.e_islandFlag, b.m_sweep.t0 = 0;
                for (h = this.m_contactList; h; h = h.m_next) h.m_flags &= ~ (q.e_toiFlag | q.e_islandFlag);
                for (h = this.m_jointList; h; h = h.m_next) h.m_islandFlag = !1;
                for (;;) {
                    var x = null,
                        d = 1;
                    for (h = this.m_contactList; h; h = h.m_next) if (!(h.IsSensor() == !0 || h.IsEnabled() == !1 || h.IsContinuous() == !1)) {
                        b = 1;
                        if (h.m_flags & q.e_toiFlag) b = h.m_toi;
                        else {
                            b = h.m_fixtureA;
                            j = h.m_fixtureB;
                            b = b.m_body;
                            j = j.m_body;
                            if ((b.GetType() != o.b2_dynamicBody || b.IsAwake() == !1) && (j.GetType() != o.b2_dynamicBody || j.IsAwake() == !1)) continue;
                            var g = b.m_sweep.t0;
                            if (b.m_sweep.t0 < j.m_sweep.t0) g = j.m_sweep.t0, b.m_sweep.Advance(g);
                            else if (j.m_sweep.t0 < b.m_sweep.t0) g = b.m_sweep.t0, j.m_sweep.Advance(g);
                            b = h.ComputeTOI(b.m_sweep, j.m_sweep);
                            l.b2Assert(0 <= b && b <= 1);
                            b > 0 && b < 1 && (b = (1 - b) * g + b, b > 1 && (b = 1));
                            h.m_toi = b;
                            h.m_flags |= q.e_toiFlag
                        }
                        Number.MIN_VALUE < b && b < d && (x = h, d = b)
                    }
                    if (x == null || 1 - 100 * Number.MIN_VALUE < d) break;
                    b = x.m_fixtureA;
                    j = x.m_fixtureB;
                    b = b.m_body;
                    j = j.m_body;
                    n.s_backupA.Set(b.m_sweep);
                    n.s_backupB.Set(j.m_sweep);
                    b.Advance(d);
                    j.Advance(d);
                    x.Update(this.m_contactManager.m_contactListener);
                    x.m_flags &= ~q.e_toiFlag;
                    if (x.IsSensor() == !0 || x.IsEnabled() == !1) b.m_sweep.Set(n.s_backupA), j.m_sweep.Set(n.s_backupB), b.SynchronizeTransform(), j.SynchronizeTransform();
                    else if (x.IsTouching() != !1) {
                        b.GetType() != o.b2_dynamicBody && (b = j);
                        c.Clear();
                        x = h = 0;
                        u[h + x++] = b;
                        for (b.m_flags |= o.e_islandFlag; x > 0;) if (b = u[h++], --x, c.AddBody(b), b.IsAwake() == !1 && b.SetAwake(!0), b.GetType() == o.b2_dynamicBody) {
                            for (j = b.m_contactList; j; j = j.next) {
                                if (c.m_contactCount == c.m_contactCapacity) break;
                                if (!(j.contact.m_flags & q.e_islandFlag) && !(j.contact.IsSensor() == !0 || j.contact.IsEnabled() == !1 || j.contact.IsTouching() == !1)) c.AddContact(j.contact), j.contact.m_flags |= q.e_islandFlag, g = j.other, g.m_flags & o.e_islandFlag || (g.GetType() != o.b2_staticBody && (g.Advance(d), g.SetAwake(!0)), u[h + x] = g, ++x, g.m_flags |= o.e_islandFlag)
                            }
                            for (b = b.m_jointList; b; b = b.next) if (c.m_jointCount != c.m_jointCapacity && b.joint.m_islandFlag != !0 && (g = b.other, g.IsActive() != !1)) c.AddJoint(b.joint), b.joint.m_islandFlag = !0, g.m_flags & o.e_islandFlag || (g.GetType() != o.b2_staticBody && (g.Advance(d), g.SetAwake(!0)), u[h + x] = g, ++x, g.m_flags |= o.e_islandFlag)
                        }
                        h = n.s_timestep;
                        h.warmStarting = !1;
                        h.dt = (1 - d) * a.dt;
                        h.inv_dt = 1 / h.dt;
                        h.dtRatio = 0;
                        h.velocityIterations = a.velocityIterations;
                        h.positionIterations = a.positionIterations;
                        c.SolveTOI(h);
                        for (d = d = 0; d < c.m_bodyCount; ++d) if (b = c.m_bodies[d], b.m_flags &= ~o.e_islandFlag, b.IsAwake() != !1 && b.GetType() == o.b2_dynamicBody) {
                            b.SynchronizeFixtures();
                            for (j = b.m_contactList; j; j = j.next) j.contact.m_flags &= ~q.e_toiFlag
                        }
                        for (d = 0; d < c.m_contactCount; ++d) h = c.m_contacts[d], h.m_flags &= ~ (q.e_toiFlag | q.e_islandFlag);
                        for (d = 0; d < c.m_jointCount; ++d) h = c.m_joints[d], h.m_islandFlag = !1;
                        this.m_contactManager.FindNewContacts()
                    }
                }
            };
            n.prototype.DrawJoint = function (a) {
                var b = a.GetBodyA(),
                    j = a.GetBodyB(),
                    h = b.m_xf.position,
                    c = j.m_xf.position,
                    q = a.GetAnchorA(),
                    u = a.GetAnchorB(),
                    d = n.s_jointColor;
                switch (a.m_type) {
                    case G.e_distanceJoint:
                        this.m_debugDraw.DrawSegment(q, u, d);
                        break;
                    case G.e_pulleyJoint:
                        b = a instanceof x ? a : null;
                        a = b.GetGroundAnchorA();
                        b = b.GetGroundAnchorB();
                        this.m_debugDraw.DrawSegment(a, q, d);
                        this.m_debugDraw.DrawSegment(b, u, d);
                        this.m_debugDraw.DrawSegment(a, b, d);
                        break;
                    case G.e_mouseJoint:
                        this.m_debugDraw.DrawSegment(q, u, d);
                        break;
                    default:
                        b != this.m_groundBody && this.m_debugDraw.DrawSegment(h, q, d), this.m_debugDraw.DrawSegment(q, u, d), j != this.m_groundBody && this.m_debugDraw.DrawSegment(c, u, d)
                }
            };
            n.prototype.DrawShape = function (a, j, h) {
                switch (a.m_type) {
                    case R.e_circleShape:
                        var c = a instanceof F ? a : null;
                        this.m_debugDraw.DrawSolidCircle(b.MulX(j, c.m_p), c.m_radius, j.R.col1, h);
                        break;
                    case R.e_polygonShape:
                        for (var c = 0, c = a instanceof y ? a : null, a = parseInt(c.GetVertexCount()), n = c.GetVertices(), q = new s(a), c = 0; c < a; ++c) q[c] = b.MulX(j, n[c]);
                        this.m_debugDraw.DrawSolidPolygon(q, a,
                        h);
                        break;
                    case R.e_edgeShape:
                        c = a instanceof I ? a : null, this.m_debugDraw.DrawSegment(b.MulX(j, c.GetVertex1()), b.MulX(j, c.GetVertex2()), h)
                }
            };
            a.postDefs.push(function () {
                a.Dynamics.b2World.s_timestep2 = new j;
                a.Dynamics.b2World.s_xf = new g;
                a.Dynamics.b2World.s_backupA = new c;
                a.Dynamics.b2World.s_backupB = new c;
                a.Dynamics.b2World.s_timestep = new j;
                a.Dynamics.b2World.s_queue = new s;
                a.Dynamics.b2World.s_jointColor = new f(0.5, 0.8, 0.8);
                a.Dynamics.b2World.e_newFixture = 1;
                a.Dynamics.b2World.e_locked = 2
            })
        })();
        (function () {
            var b = a.Collision.Shapes.b2CircleShape,
                c = a.Collision.Shapes.b2EdgeShape,
                g = a.Collision.Shapes.b2PolygonShape,
                p = a.Collision.Shapes.b2Shape,
                f = a.Dynamics.Contacts.b2CircleContact,
                l = a.Dynamics.Contacts.b2Contact,
                m = a.Dynamics.Contacts.b2ContactConstraint,
                d = a.Dynamics.Contacts.b2ContactConstraintPoint,
                i = a.Dynamics.Contacts.b2ContactEdge,
                A = a.Dynamics.Contacts.b2ContactFactory,
                B = a.Dynamics.Contacts.b2ContactRegister,
                F = a.Dynamics.Contacts.b2ContactResult,
                I = a.Dynamics.Contacts.b2ContactSolver,
                w = a.Dynamics.Contacts.b2EdgeAndCircleContact,
                y = a.Dynamics.Contacts.b2NullContact,
                R = a.Dynamics.Contacts.b2PolyAndCircleContact,
                o = a.Dynamics.Contacts.b2PolyAndEdgeContact,
                M = a.Dynamics.Contacts.b2PolygonContact,
                D = a.Dynamics.Contacts.b2PositionSolverManifold,
                N = a.Dynamics.b2Body,
                J = a.Dynamics.b2TimeStep,
                K = a.Common.b2Settings,
                C = a.Common.Math.b2Mat22,
                O = a.Common.Math.b2Math,
                E = a.Common.Math.b2Vec2,
                H = a.Collision.b2Collision,
                P = a.Collision.b2ContactID,
                L = a.Collision.b2Manifold,
                j = a.Collision.b2TimeOfImpact,
                n = a.Collision.b2TOIInput,
                q = a.Collision.b2WorldManifold;
            a.inherit(f, a.Dynamics.Contacts.b2Contact);
            f.prototype.__super = a.Dynamics.Contacts.b2Contact.prototype;
            f.b2CircleContact = function () {
                a.Dynamics.Contacts.b2Contact.b2Contact.apply(this, arguments)
            };
            f.Create = function () {
                return new f
            };
            f.Destroy = function () {};
            f.prototype.Reset = function (a, b) {
                this.__super.Reset.call(this, a, b)
            };
            f.prototype.Evaluate = function () {
                var a = this.m_fixtureA.GetBody(),
                    j = this.m_fixtureB.GetBody();
                H.CollideCircles(this.m_manifold, this.m_fixtureA.GetShape() instanceof b ? this.m_fixtureA.GetShape() : null, a.m_xf, this.m_fixtureB.GetShape() instanceof b ? this.m_fixtureB.GetShape() : null, j.m_xf)
            };
            l.b2Contact = function () {
                this.m_nodeA = new i;
                this.m_nodeB = new i;
                this.m_manifold = new L;
                this.m_oldManifold = new L
            };
            l.prototype.GetManifold = function () {
                return this.m_manifold
            };
            l.prototype.GetWorldManifold = function (a) {
                var b = this.m_fixtureA.GetBody(),
                    j = this.m_fixtureB.GetBody(),
                    c = this.m_fixtureA.GetShape(),
                    e = this.m_fixtureB.GetShape();
                a.Initialize(this.m_manifold, b.GetTransform(), c.m_radius, j.GetTransform(), e.m_radius)
            };
            l.prototype.IsTouching = function () {
                return (this.m_flags & l.e_touchingFlag) == l.e_touchingFlag
            };
            l.prototype.IsContinuous = function () {
                return (this.m_flags & l.e_continuousFlag) == l.e_continuousFlag
            };
            l.prototype.SetSensor = function (a) {
                a ? this.m_flags |= l.e_sensorFlag : this.m_flags &= ~l.e_sensorFlag
            };
            l.prototype.IsSensor = function () {
                return (this.m_flags & l.e_sensorFlag) == l.e_sensorFlag
            };
            l.prototype.SetEnabled = function (a) {
                a ? this.m_flags |= l.e_enabledFlag : this.m_flags &= ~l.e_enabledFlag
            };
            l.prototype.IsEnabled = function () {
                return (this.m_flags & l.e_enabledFlag) == l.e_enabledFlag
            };
            l.prototype.GetNext = function () {
                return this.m_next
            };
            l.prototype.GetFixtureA = function () {
                return this.m_fixtureA
            };
            l.prototype.GetFixtureB = function () {
                return this.m_fixtureB
            };
            l.prototype.FlagForFiltering = function () {
                this.m_flags |= l.e_filterFlag
            };
            l.prototype.b2Contact = function () {};
            l.prototype.Reset = function (a, b) {
                a === void 0 && (a = null);
                b === void 0 && (b = null);
                this.m_flags = l.e_enabledFlag;
                if (!a || !b) this.m_fixtureB = this.m_fixtureA = null;
                else {
                    if (a.IsSensor() || b.IsSensor()) this.m_flags |= l.e_sensorFlag;
                    var j = a.GetBody(),
                        c = b.GetBody();
                    if (j.GetType() != N.b2_dynamicBody || j.IsBullet() || c.GetType() != N.b2_dynamicBody || c.IsBullet()) this.m_flags |= l.e_continuousFlag;
                    this.m_fixtureA = a;
                    this.m_fixtureB = b;
                    this.m_manifold.m_pointCount = 0;
                    this.m_next = this.m_prev = null;
                    this.m_nodeA.contact = null;
                    this.m_nodeA.prev = null;
                    this.m_nodeA.next = null;
                    this.m_nodeA.other = null;
                    this.m_nodeB.contact = null;
                    this.m_nodeB.prev = null;
                    this.m_nodeB.next = null;
                    this.m_nodeB.other = null
                }
            };
            l.prototype.Update = function (a) {
                var b = this.m_oldManifold;
                this.m_oldManifold = this.m_manifold;
                this.m_manifold = b;
                this.m_flags |= l.e_enabledFlag;
                var j = !1,
                    b = (this.m_flags & l.e_touchingFlag) == l.e_touchingFlag,
                    c = this.m_fixtureA.m_body,
                    e = this.m_fixtureB.m_body,
                    n = this.m_fixtureA.m_aabb.TestOverlap(this.m_fixtureB.m_aabb);
                if (this.m_flags & l.e_sensorFlag) n && (j = this.m_fixtureA.GetShape(), n = this.m_fixtureB.GetShape(), c = c.GetTransform(), e = e.GetTransform(), j = p.TestOverlap(j, c, n, e)), this.m_manifold.m_pointCount = 0;
                else {
                    c.GetType() != N.b2_dynamicBody || c.IsBullet() || e.GetType() != N.b2_dynamicBody || e.IsBullet() ? this.m_flags |= l.e_continuousFlag : this.m_flags &= ~l.e_continuousFlag;
                    if (n) {
                        this.Evaluate();
                        j = this.m_manifold.m_pointCount > 0;
                        for (n = 0; n < this.m_manifold.m_pointCount; ++n) {
                            var q = this.m_manifold.m_points[n];
                            q.m_normalImpulse = 0;
                            q.m_tangentImpulse = 0;
                            for (var d = q.m_id, g = 0; g < this.m_oldManifold.m_pointCount; ++g) {
                                var f = this.m_oldManifold.m_points[g];
                                if (f.m_id.key == d.key) {
                                    q.m_normalImpulse = f.m_normalImpulse;
                                    q.m_tangentImpulse = f.m_tangentImpulse;
                                    break
                                }
                            }
                        }
                    } else this.m_manifold.m_pointCount = 0;
                    j != b && (c.SetAwake(!0), e.SetAwake(!0))
                }
                j ? this.m_flags |= l.e_touchingFlag : this.m_flags &= ~l.e_touchingFlag;
                b == !1 && j == !0 && a.BeginContact(this);
                b == !0 && j == !1 && a.EndContact(this);
                (this.m_flags & l.e_sensorFlag) == 0 && a.PreSolve(this, this.m_oldManifold)
            };
            l.prototype.Evaluate = function () {};
            l.prototype.ComputeTOI = function (a, b) {
                l.s_input.proxyA.Set(this.m_fixtureA.GetShape());
                l.s_input.proxyB.Set(this.m_fixtureB.GetShape());
                l.s_input.sweepA = a;
                l.s_input.sweepB = b;
                l.s_input.tolerance = K.b2_linearSlop;
                return j.TimeOfImpact(l.s_input)
            };
            a.postDefs.push(function () {
                a.Dynamics.Contacts.b2Contact.e_sensorFlag = 1;
                a.Dynamics.Contacts.b2Contact.e_continuousFlag = 2;
                a.Dynamics.Contacts.b2Contact.e_islandFlag = 4;
                a.Dynamics.Contacts.b2Contact.e_toiFlag = 8;
                a.Dynamics.Contacts.b2Contact.e_touchingFlag = 16;
                a.Dynamics.Contacts.b2Contact.e_enabledFlag = 32;
                a.Dynamics.Contacts.b2Contact.e_filterFlag = 64;
                a.Dynamics.Contacts.b2Contact.s_input = new n
            });
            m.b2ContactConstraint = function () {
                this.localPlaneNormal = new E;
                this.localPoint = new E;
                this.normal = new E;
                this.normalMass = new C;
                this.K = new C
            };
            m.prototype.b2ContactConstraint = function () {
                this.points = new s(K.b2_maxManifoldPoints);
                for (var a = 0; a < K.b2_maxManifoldPoints; a++) this.points[a] = new d
            };
            d.b2ContactConstraintPoint = function () {
                this.localPoint = new E;
                this.rA = new E;
                this.rB = new E
            };
            i.b2ContactEdge = function () {};
            A.b2ContactFactory = function () {};
            A.prototype.b2ContactFactory = function (a) {
                this.m_allocator = a;
                this.InitializeRegisters()
            };
            A.prototype.AddType = function (a, b, j, c) {
                j === void 0 && (j = 0);
                c === void 0 && (c = 0);
                this.m_registers[j][c].createFcn = a;
                this.m_registers[j][c].destroyFcn = b;
                this.m_registers[j][c].primary = !0;
                if (j != c) this.m_registers[c][j].createFcn = a, this.m_registers[c][j].destroyFcn = b, this.m_registers[c][j].primary = !1
            };
            A.prototype.InitializeRegisters = function () {
                this.m_registers = new s(p.e_shapeTypeCount);
                for (var a = 0; a < p.e_shapeTypeCount; a++) {
                    this.m_registers[a] = new s(p.e_shapeTypeCount);
                    for (var b = 0; b < p.e_shapeTypeCount; b++) this.m_registers[a][b] = new B
                }
                this.AddType(f.Create, f.Destroy, p.e_circleShape, p.e_circleShape);
                this.AddType(R.Create,
                R.Destroy, p.e_polygonShape, p.e_circleShape);
                this.AddType(M.Create, M.Destroy, p.e_polygonShape, p.e_polygonShape);
                this.AddType(w.Create, w.Destroy, p.e_edgeShape, p.e_circleShape);
                this.AddType(o.Create, o.Destroy, p.e_polygonShape, p.e_edgeShape)
            };
            A.prototype.Create = function (a, b) {
                var j = parseInt(a.GetType()),
                    c = parseInt(b.GetType()),
                    j = this.m_registers[j][c];
                if (j.pool) return c = j.pool, j.pool = c.m_next, j.poolCount--, c.Reset(a, b), c;
                c = j.createFcn;
                return c != null ? (j.primary ? (c = c(this.m_allocator), c.Reset(a, b)) : (c = c(this.m_allocator),
                c.Reset(b, a)), c) : null
            };
            A.prototype.Destroy = function (a) {
                a.m_manifold.m_pointCount > 0 && (a.m_fixtureA.m_body.SetAwake(!0), a.m_fixtureB.m_body.SetAwake(!0));
                var b = parseInt(a.m_fixtureA.GetType()),
                    j = parseInt(a.m_fixtureB.GetType()),
                    b = this.m_registers[b][j];
                b.poolCount++;
                a.m_next = b.pool;
                b.pool = a;
                b = b.destroyFcn;
                b(a, this.m_allocator)
            };
            B.b2ContactRegister = function () {};
            F.b2ContactResult = function () {
                this.position = new E;
                this.normal = new E;
                this.id = new P
            };
            I.b2ContactSolver = function () {
                this.m_step = new J;
                this.m_constraints = new s
            };
            I.prototype.b2ContactSolver = function () {};
            I.prototype.Initialize = function (a, b, j, c) {
                j === void 0 && (j = 0);
                var e;
                this.m_step.Set(a);
                this.m_allocator = c;
                a = 0;
                for (this.m_constraintCount = j; this.m_constraints.length < this.m_constraintCount;) this.m_constraints[this.m_constraints.length] = new m;
                for (a = 0; a < j; ++a) {
                    e = b[a];
                    var c = e.m_fixtureA,
                        n = e.m_fixtureB,
                        q = c.m_shape.m_radius,
                        d = n.m_shape.m_radius,
                        g = c.m_body,
                        f = n.m_body,
                        p = e.GetManifold(),
                        l = K.b2MixFriction(c.GetFriction(), n.GetFriction()),
                        o = K.b2MixRestitution(c.GetRestitution(),
                        n.GetRestitution()),
                        i = g.m_linearVelocity.x,
                        r = g.m_linearVelocity.y,
                        t = f.m_linearVelocity.x,
                        s = f.m_linearVelocity.y,
                        B = g.m_angularVelocity,
                        w = f.m_angularVelocity;
                    K.b2Assert(p.m_pointCount > 0);
                    I.s_worldManifold.Initialize(p, g.m_xf, q, f.m_xf, d);
                    n = I.s_worldManifold.m_normal.x;
                    e = I.s_worldManifold.m_normal.y;
                    c = this.m_constraints[a];
                    c.bodyA = g;
                    c.bodyB = f;
                    c.manifold = p;
                    c.normal.x = n;
                    c.normal.y = e;
                    c.pointCount = p.m_pointCount;
                    c.friction = l;
                    c.restitution = o;
                    c.localPlaneNormal.x = p.m_localPlaneNormal.x;
                    c.localPlaneNormal.y = p.m_localPlaneNormal.y;
                    c.localPoint.x = p.m_localPoint.x;
                    c.localPoint.y = p.m_localPoint.y;
                    c.radius = q + d;
                    c.type = p.m_type;
                    for (q = 0; q < c.pointCount; ++q) {
                        l = p.m_points[q];
                        d = c.points[q];
                        d.normalImpulse = l.m_normalImpulse;
                        d.tangentImpulse = l.m_tangentImpulse;
                        d.localPoint.SetV(l.m_localPoint);
                        var l = d.rA.x = I.s_worldManifold.m_points[q].x - g.m_sweep.c.x,
                            o = d.rA.y = I.s_worldManifold.m_points[q].y - g.m_sweep.c.y,
                            A = d.rB.x = I.s_worldManifold.m_points[q].x - f.m_sweep.c.x,
                            F = d.rB.y = I.s_worldManifold.m_points[q].y - f.m_sweep.c.y,
                            y = l * e - o * n,
                            D = A * e - F * n;
                        y *= y;
                        D *= D;
                        d.normalMass = 1 / (g.m_invMass + f.m_invMass + g.m_invI * y + f.m_invI * D);
                        var C = g.m_mass * g.m_invMass + f.m_mass * f.m_invMass;
                        C += g.m_mass * g.m_invI * y + f.m_mass * f.m_invI * D;
                        d.equalizedMass = 1 / C;
                        D = e;
                        C = -n;
                        y = l * C - o * D;
                        D = A * C - F * D;
                        y *= y;
                        D *= D;
                        d.tangentMass = 1 / (g.m_invMass + f.m_invMass + g.m_invI * y + f.m_invI * D);
                        d.velocityBias = 0;
                        l = c.normal.x * (t + -w * F - i - -B * o) + c.normal.y * (s + w * A - r - B * l);
                        l < -K.b2_velocityThreshold && (d.velocityBias += -c.restitution * l)
                    }
                    if (c.pointCount == 2) s = c.points[0], t = c.points[1], p = g.m_invMass,
                    g = g.m_invI, i = f.m_invMass, f = f.m_invI, r = s.rA.x * e - s.rA.y * n, s = s.rB.x * e - s.rB.y * n, B = t.rA.x * e - t.rA.y * n, t = t.rB.x * e - t.rB.y * n, n = p + i + g * r * r + f * s * s, e = p + i + g * B * B + f * t * t, f = p + i + g * r * B + f * s * t, n * n < 100 * (n * e - f * f) ? (c.K.col1.Set(n, f), c.K.col2.Set(f, e), c.K.GetInverse(c.normalMass)) : c.pointCount = 1
                }
            };
            I.prototype.InitVelocityConstraints = function (a) {
                for (var b = 0; b < this.m_constraintCount; ++b) {
                    var j = this.m_constraints[b],
                        c = j.bodyA,
                        e = j.bodyB,
                        n = c.m_invMass,
                        q = c.m_invI,
                        d = e.m_invMass,
                        g = e.m_invI,
                        f = j.normal.x,
                        p = j.normal.y,
                        l = p,
                        m = -f,
                        o = 0,
                        i = 0;
                    if (a.warmStarting) {
                        i = j.pointCount;
                        for (o = 0; o < i; ++o) {
                            var r = j.points[o];
                            r.normalImpulse *= a.dtRatio;
                            r.tangentImpulse *= a.dtRatio;
                            var t = r.normalImpulse * f + r.tangentImpulse * l,
                                s = r.normalImpulse * p + r.tangentImpulse * m;
                            c.m_angularVelocity -= q * (r.rA.x * s - r.rA.y * t);
                            c.m_linearVelocity.x -= n * t;
                            c.m_linearVelocity.y -= n * s;
                            e.m_angularVelocity += g * (r.rB.x * s - r.rB.y * t);
                            e.m_linearVelocity.x += d * t;
                            e.m_linearVelocity.y += d * s
                        }
                    } else {
                        i = j.pointCount;
                        for (o = 0; o < i; ++o) c = j.points[o], c.normalImpulse = 0, c.tangentImpulse = 0
                    }
                }
            };
            I.prototype.SolveVelocityConstraints = function () {
                for (var a = 0, b, j = 0, c = 0, e = 0, n = c = c = j = j = 0, q = j = j = 0, d = j = e = 0, g = 0, f, p = 0; p < this.m_constraintCount; ++p) {
                    var e = this.m_constraints[p],
                        l = e.bodyA,
                        m = e.bodyB,
                        o = l.m_angularVelocity,
                        i = m.m_angularVelocity,
                        r = l.m_linearVelocity,
                        t = m.m_linearVelocity,
                        s = l.m_invMass,
                        z = l.m_invI,
                        B = m.m_invMass,
                        w = m.m_invI,
                        d = e.normal.x,
                        A = g = e.normal.y;
                    f = -d;
                    q = e.friction;
                    for (a = 0; a < e.pointCount; a++) b = e.points[a], j = t.x - i * b.rB.y - r.x + o * b.rA.y, c = t.y + i * b.rB.x - r.y - o * b.rA.x, j = j * A + c * f, j = b.tangentMass * -j, c = q * b.normalImpulse, c = O.Clamp(b.tangentImpulse + j, -c, c), j = c - b.tangentImpulse, n = j * A, j *= f, r.x -= s * n, r.y -= s * j, o -= z * (b.rA.x * j - b.rA.y * n), t.x += B * n, t.y += B * j, i += w * (b.rB.x * j - b.rB.y * n), b.tangentImpulse = c;
                    parseInt(e.pointCount);
                    if (e.pointCount == 1) b = e.points[0], j = t.x + -i * b.rB.y - r.x - -o * b.rA.y, c = t.y + i * b.rB.x - r.y - o * b.rA.x, e = j * d + c * g, j = -b.normalMass * (e - b.velocityBias), c = b.normalImpulse + j, c = c > 0 ? c : 0, j = c - b.normalImpulse, n = j * d, j *= g, r.x -= s * n, r.y -= s * j, o -= z * (b.rA.x * j - b.rA.y * n), t.x += B * n, t.y += B * j, i += w * (b.rB.x * j - b.rB.y * n), b.normalImpulse = c;
                    else {
                        b = e.points[0];
                        var a = e.points[1],
                            j = b.normalImpulse,
                            q = a.normalImpulse,
                            I = (t.x - i * b.rB.y - r.x + o * b.rA.y) * d + (t.y + i * b.rB.x - r.y - o * b.rA.x) * g,
                            F = (t.x - i * a.rB.y - r.x + o * a.rA.y) * d + (t.y + i * a.rB.x - r.y - o * a.rA.x) * g,
                            c = I - b.velocityBias,
                            n = F - a.velocityBias;
                        f = e.K;
                        c -= f.col1.x * j + f.col2.x * q;
                        for (n -= f.col1.y * j + f.col2.y * q;;) {
                            f = e.normalMass;
                            A = -(f.col1.x * c + f.col2.x * n);
                            f = -(f.col1.y * c + f.col2.y * n);
                            if (A >= 0 && f >= 0) {
                                j = A - j;
                                q = f - q;
                                e = j * d;
                                j *= g;
                                d *= q;
                                g *= q;
                                r.x -= s * (e + d);
                                r.y -= s * (j + g);
                                o -= z * (b.rA.x * j - b.rA.y * e + a.rA.x * g - a.rA.y * d);
                                t.x += B * (e + d);
                                t.y += B * (j + g);
                                i += w * (b.rB.x * j - b.rB.y * e + a.rB.x * g - a.rB.y * d);
                                b.normalImpulse = A;
                                a.normalImpulse = f;
                                break
                            }
                            A = -b.normalMass * c;
                            f = 0;
                            F = e.K.col1.y * A + n;
                            if (A >= 0 && F >= 0) {
                                j = A - j;
                                q = f - q;
                                e = j * d;
                                j *= g;
                                d *= q;
                                g *= q;
                                r.x -= s * (e + d);
                                r.y -= s * (j + g);
                                o -= z * (b.rA.x * j - b.rA.y * e + a.rA.x * g - a.rA.y * d);
                                t.x += B * (e + d);
                                t.y += B * (j + g);
                                i += w * (b.rB.x * j - b.rB.y * e + a.rB.x * g - a.rB.y * d);
                                b.normalImpulse = A;
                                a.normalImpulse = f;
                                break
                            }
                            A = 0;
                            f = -a.normalMass * n;
                            I = e.K.col2.x * f + c;
                            if (f >= 0 && I >= 0) {
                                j = A - j;
                                q = f - q;
                                e = j * d;
                                j *= g;
                                d *= q;
                                g *= q;
                                r.x -= s * (e + d);
                                r.y -= s * (j + g);
                                o -= z * (b.rA.x * j - b.rA.y * e + a.rA.x * g - a.rA.y * d);
                                t.x += B * (e + d);
                                t.y += B * (j + g);
                                i += w * (b.rB.x * j - b.rB.y * e + a.rB.x * g - a.rB.y * d);
                                b.normalImpulse = A;
                                a.normalImpulse = f;
                                break
                            }
                            f = A = 0;
                            I = c;
                            F = n;
                            if (I >= 0 && F >= 0) {
                                j = A - j;
                                q = f - q;
                                e = j * d;
                                j *= g;
                                d *= q;
                                g *= q;
                                r.x -= s * (e + d);
                                r.y -= s * (j + g);
                                o -= z * (b.rA.x * j - b.rA.y * e + a.rA.x * g - a.rA.y * d);
                                t.x += B * (e + d);
                                t.y += B * (j + g);
                                i += w * (b.rB.x * j - b.rB.y * e + a.rB.x * g - a.rB.y * d);
                                b.normalImpulse = A;
                                a.normalImpulse = f;
                                break
                            }
                            break
                        }
                    }
                    l.m_angularVelocity = o;
                    m.m_angularVelocity = i
                }
            };
            I.prototype.FinalizeVelocityConstraints = function () {
                for (var a = 0; a < this.m_constraintCount; ++a) for (var b = this.m_constraints[a],
                j = b.manifold, c = 0; c < b.pointCount; ++c) {
                    var e = j.m_points[c],
                        n = b.points[c];
                    e.m_normalImpulse = n.normalImpulse;
                    e.m_tangentImpulse = n.tangentImpulse
                }
            };
            I.prototype.SolvePositionConstraints = function (a) {
                a === void 0 && (a = 0);
                for (var b = 0, j = 0; j < this.m_constraintCount; j++) {
                    var c = this.m_constraints[j],
                        n = c.bodyA,
                        q = c.bodyB,
                        d = n.m_mass * n.m_invMass,
                        g = n.m_mass * n.m_invI,
                        f = q.m_mass * q.m_invMass,
                        p = q.m_mass * q.m_invI;
                    I.s_psm.Initialize(c);
                    for (var l = I.s_psm.m_normal, m = 0; m < c.pointCount; m++) {
                        var o = c.points[m],
                            i = I.s_psm.m_points[m],
                            r = I.s_psm.m_separations[m],
                            t = i.x - n.m_sweep.c.x,
                            s = i.y - n.m_sweep.c.y,
                            z = i.x - q.m_sweep.c.x,
                            i = i.y - q.m_sweep.c.y,
                            b = b < r ? b : r,
                            r = O.Clamp(a * (r + K.b2_linearSlop), -K.b2_maxLinearCorrection, 0);
                        r *= -o.equalizedMass;
                        o = r * l.x;
                        r *= l.y;
                        n.m_sweep.c.x -= d * o;
                        n.m_sweep.c.y -= d * r;
                        n.m_sweep.a -= g * (t * r - s * o);
                        n.SynchronizeTransform();
                        q.m_sweep.c.x += f * o;
                        q.m_sweep.c.y += f * r;
                        q.m_sweep.a += p * (z * r - i * o);
                        q.SynchronizeTransform()
                    }
                }
                return b > -1.5 * K.b2_linearSlop
            };
            a.postDefs.push(function () {
                a.Dynamics.Contacts.b2ContactSolver.s_worldManifold = new q;
                a.Dynamics.Contacts.b2ContactSolver.s_psm = new D
            });
            a.inherit(w, a.Dynamics.Contacts.b2Contact);
            w.prototype.__super = a.Dynamics.Contacts.b2Contact.prototype;
            w.b2EdgeAndCircleContact = function () {
                a.Dynamics.Contacts.b2Contact.b2Contact.apply(this, arguments)
            };
            w.Create = function () {
                return new w
            };
            w.Destroy = function () {};
            w.prototype.Reset = function (a, b) {
                this.__super.Reset.call(this, a, b)
            };
            w.prototype.Evaluate = function () {
                var a = this.m_fixtureA.GetBody(),
                    j = this.m_fixtureB.GetBody();
                this.b2CollideEdgeAndCircle(this.m_manifold,
                this.m_fixtureA.GetShape() instanceof c ? this.m_fixtureA.GetShape() : null, a.m_xf, this.m_fixtureB.GetShape() instanceof b ? this.m_fixtureB.GetShape() : null, j.m_xf)
            };
            w.prototype.b2CollideEdgeAndCircle = function () {};
            a.inherit(y, a.Dynamics.Contacts.b2Contact);
            y.prototype.__super = a.Dynamics.Contacts.b2Contact.prototype;
            y.b2NullContact = function () {
                a.Dynamics.Contacts.b2Contact.b2Contact.apply(this, arguments)
            };
            y.prototype.b2NullContact = function () {
                this.__super.b2Contact.call(this)
            };
            y.prototype.Evaluate = function () {};
            a.inherit(R, a.Dynamics.Contacts.b2Contact);
            R.prototype.__super = a.Dynamics.Contacts.b2Contact.prototype;
            R.b2PolyAndCircleContact = function () {
                a.Dynamics.Contacts.b2Contact.b2Contact.apply(this, arguments)
            };
            R.Create = function () {
                return new R
            };
            R.Destroy = function () {};
            R.prototype.Reset = function (a, b) {
                this.__super.Reset.call(this, a, b);
                K.b2Assert(a.GetType() == p.e_polygonShape);
                K.b2Assert(b.GetType() == p.e_circleShape)
            };
            R.prototype.Evaluate = function () {
                var a = this.m_fixtureA.m_body,
                    j = this.m_fixtureB.m_body;
                H.CollidePolygonAndCircle(this.m_manifold,
                this.m_fixtureA.GetShape() instanceof g ? this.m_fixtureA.GetShape() : null, a.m_xf, this.m_fixtureB.GetShape() instanceof b ? this.m_fixtureB.GetShape() : null, j.m_xf)
            };
            a.inherit(o, a.Dynamics.Contacts.b2Contact);
            o.prototype.__super = a.Dynamics.Contacts.b2Contact.prototype;
            o.b2PolyAndEdgeContact = function () {
                a.Dynamics.Contacts.b2Contact.b2Contact.apply(this, arguments)
            };
            o.Create = function () {
                return new o
            };
            o.Destroy = function () {};
            o.prototype.Reset = function (a, b) {
                this.__super.Reset.call(this, a, b);
                K.b2Assert(a.GetType() == p.e_polygonShape);
                K.b2Assert(b.GetType() == p.e_edgeShape)
            };
            o.prototype.Evaluate = function () {
                var a = this.m_fixtureA.GetBody(),
                    b = this.m_fixtureB.GetBody();
                this.b2CollidePolyAndEdge(this.m_manifold, this.m_fixtureA.GetShape() instanceof g ? this.m_fixtureA.GetShape() : null, a.m_xf, this.m_fixtureB.GetShape() instanceof c ? this.m_fixtureB.GetShape() : null, b.m_xf)
            };
            o.prototype.b2CollidePolyAndEdge = function () {};
            a.inherit(M, a.Dynamics.Contacts.b2Contact);
            M.prototype.__super = a.Dynamics.Contacts.b2Contact.prototype;
            M.b2PolygonContact = function () {
                a.Dynamics.Contacts.b2Contact.b2Contact.apply(this, arguments)
            };
            M.Create = function () {
                return new M
            };
            M.Destroy = function () {};
            M.prototype.Reset = function (a, b) {
                this.__super.Reset.call(this, a, b)
            };
            M.prototype.Evaluate = function () {
                var a = this.m_fixtureA.GetBody(),
                    b = this.m_fixtureB.GetBody();
                H.CollidePolygons(this.m_manifold, this.m_fixtureA.GetShape() instanceof g ? this.m_fixtureA.GetShape() : null, a.m_xf, this.m_fixtureB.GetShape() instanceof g ? this.m_fixtureB.GetShape() : null, b.m_xf)
            };
            D.b2PositionSolverManifold = function () {};
            D.prototype.b2PositionSolverManifold = function () {
                this.m_normal = new E;
                this.m_separations = new r(K.b2_maxManifoldPoints);
                this.m_points = new s(K.b2_maxManifoldPoints);
                for (var a = 0; a < K.b2_maxManifoldPoints; a++) this.m_points[a] = new E
            };
            D.prototype.Initialize = function (a) {
                K.b2Assert(a.pointCount > 0);
                var b = 0,
                    j = 0,
                    c = 0,
                    n, q = 0,
                    d = 0;
                switch (a.type) {
                    case L.e_circles:
                        n = a.bodyA.m_xf.R;
                        c = a.localPoint;
                        b = a.bodyA.m_xf.position.x + (n.col1.x * c.x + n.col2.x * c.y);
                        j = a.bodyA.m_xf.position.y + (n.col1.y * c.x + n.col2.y * c.y);
                        n = a.bodyB.m_xf.R;
                        c = a.points[0].localPoint;
                        q = a.bodyB.m_xf.position.x + (n.col1.x * c.x + n.col2.x * c.y);
                        n = a.bodyB.m_xf.position.y + (n.col1.y * c.x + n.col2.y * c.y);
                        var c = q - b,
                            d = n - j,
                            g = c * c + d * d;
                        g > Number.MIN_VALUE * Number.MIN_VALUE ? (g = Math.sqrt(g), this.m_normal.x = c / g, this.m_normal.y = d / g) : (this.m_normal.x = 1, this.m_normal.y = 0);
                        this.m_points[0].x = 0.5 * (b + q);
                        this.m_points[0].y = 0.5 * (j + n);
                        this.m_separations[0] = c * this.m_normal.x + d * this.m_normal.y - a.radius;
                        break;
                    case L.e_faceA:
                        n = a.bodyA.m_xf.R;
                        c = a.localPlaneNormal;
                        this.m_normal.x = n.col1.x * c.x + n.col2.x * c.y;
                        this.m_normal.y = n.col1.y * c.x + n.col2.y * c.y;
                        n = a.bodyA.m_xf.R;
                        c = a.localPoint;
                        q = a.bodyA.m_xf.position.x + (n.col1.x * c.x + n.col2.x * c.y);
                        d = a.bodyA.m_xf.position.y + (n.col1.y * c.x + n.col2.y * c.y);
                        n = a.bodyB.m_xf.R;
                        for (b = 0; b < a.pointCount; ++b) c = a.points[b].localPoint, j = a.bodyB.m_xf.position.x + (n.col1.x * c.x + n.col2.x * c.y), c = a.bodyB.m_xf.position.y + (n.col1.y * c.x + n.col2.y * c.y), this.m_separations[b] = (j - q) * this.m_normal.x + (c - d) * this.m_normal.y - a.radius, this.m_points[b].x = j, this.m_points[b].y = c;
                        break;
                    case L.e_faceB:
                        n = a.bodyB.m_xf.R;
                        c = a.localPlaneNormal;
                        this.m_normal.x = n.col1.x * c.x + n.col2.x * c.y;
                        this.m_normal.y = n.col1.y * c.x + n.col2.y * c.y;
                        n = a.bodyB.m_xf.R;
                        c = a.localPoint;
                        q = a.bodyB.m_xf.position.x + (n.col1.x * c.x + n.col2.x * c.y);
                        d = a.bodyB.m_xf.position.y + (n.col1.y * c.x + n.col2.y * c.y);
                        n = a.bodyA.m_xf.R;
                        for (b = 0; b < a.pointCount; ++b) c = a.points[b].localPoint, j = a.bodyA.m_xf.position.x + (n.col1.x * c.x + n.col2.x * c.y), c = a.bodyA.m_xf.position.y + (n.col1.y * c.x + n.col2.y * c.y), this.m_separations[b] = (j - q) * this.m_normal.x + (c - d) * this.m_normal.y - a.radius, this.m_points[b].Set(j, c);
                        this.m_normal.x *= -1;
                        this.m_normal.y *= -1
                }
            };
            a.postDefs.push(function () {
                a.Dynamics.Contacts.b2PositionSolverManifold.circlePointA = new E;
                a.Dynamics.Contacts.b2PositionSolverManifold.circlePointB = new E
            })
        })();
        (function () {
            var b = a.Common.Math.b2Mat22,
                c = a.Common.Math.b2Math,
                g = a.Common.Math.b2Vec2,
                p = a.Common.b2Color,
                f = a.Dynamics.Controllers.b2BuoyancyController,
                l = a.Dynamics.Controllers.b2ConstantAccelController,
                m = a.Dynamics.Controllers.b2ConstantForceController,
                d = a.Dynamics.Controllers.b2Controller,
                i = a.Dynamics.Controllers.b2ControllerEdge,
                r = a.Dynamics.Controllers.b2GravityController,
                s = a.Dynamics.Controllers.b2TensorDampingController;
            a.inherit(f, a.Dynamics.Controllers.b2Controller);
            f.prototype.__super = a.Dynamics.Controllers.b2Controller.prototype;
            f.b2BuoyancyController = function () {
                a.Dynamics.Controllers.b2Controller.b2Controller.apply(this, arguments);
                this.normal = new g(0, -1);
                this.density = this.offset = 0;
                this.velocity = new g(0, 0);
                this.linearDrag = 2;
                this.angularDrag = 1;
                this.useDensity = !1;
                this.useWorldGravity = !0;
                this.gravity = null
            };
            f.prototype.Step = function () {
                if (this.m_bodyList) {
                    if (this.useWorldGravity) this.gravity = this.GetWorld().GetGravity().Copy();
                    for (var a = this.m_bodyList; a; a = a.nextBody) {
                        var b = a.body;
                        if (b.IsAwake() != !1) {
                            for (var c = new g, d = new g, f = 0, p = 0, l = b.GetFixtureList(); l; l = l.GetNext()) {
                                var m = new g,
                                    i = l.GetShape().ComputeSubmergedArea(this.normal, this.offset, b.GetTransform(), m);
                                f += i;
                                c.x += i * m.x;
                                c.y += i * m.y;
                                var r = 0,
                                    r = 1;
                                p += i * r;
                                d.x += i * m.x * r;
                                d.y += i * m.y * r
                            }
                            c.x /= f;
                            c.y /= f;
                            d.x /= p;
                            d.y /= p;
                            f < Number.MIN_VALUE || (p = this.gravity.GetNegative(), p.Multiply(this.density * f), b.ApplyForce(p, d), d = b.GetLinearVelocityFromWorldPoint(c), d.Subtract(this.velocity), d.Multiply(-this.linearDrag * f), b.ApplyForce(d, c), b.ApplyTorque(-b.GetInertia() / b.GetMass() * f * b.GetAngularVelocity() * this.angularDrag))
                        }
                    }
                }
            };
            f.prototype.Draw = function (a) {
                var b = new g,
                    c = new g;
                b.x = this.normal.x * this.offset + this.normal.y * 1E3;
                b.y = this.normal.y * this.offset - this.normal.x * 1E3;
                c.x = this.normal.x * this.offset - this.normal.y * 1E3;
                c.y = this.normal.y * this.offset + this.normal.x * 1E3;
                var d = new p(0, 0, 1);
                a.DrawSegment(b, c, d)
            };
            a.inherit(l, a.Dynamics.Controllers.b2Controller);
            l.prototype.__super = a.Dynamics.Controllers.b2Controller.prototype;
            l.b2ConstantAccelController = function () {
                a.Dynamics.Controllers.b2Controller.b2Controller.apply(this, arguments);
                this.A = new g(0, 0)
            };
            l.prototype.Step = function (a) {
                for (var a = new g(this.A.x * a.dt, this.A.y * a.dt), b = this.m_bodyList; b; b = b.nextBody) {
                    var c = b.body;
                    c.IsAwake() && c.SetLinearVelocity(new g(c.GetLinearVelocity().x + a.x, c.GetLinearVelocity().y + a.y))
                }
            };
            a.inherit(m, a.Dynamics.Controllers.b2Controller);
            m.prototype.__super = a.Dynamics.Controllers.b2Controller.prototype;
            m.b2ConstantForceController = function () {
                a.Dynamics.Controllers.b2Controller.b2Controller.apply(this, arguments);
                this.F = new g(0, 0)
            };
            m.prototype.Step = function () {
                for (var a = this.m_bodyList; a; a = a.nextBody) {
                    var b = a.body;
                    b.IsAwake() && b.ApplyForce(this.F, b.GetWorldCenter())
                }
            };
            d.b2Controller = function () {};
            d.prototype.Step = function () {};
            d.prototype.Draw = function () {};
            d.prototype.AddBody = function (a) {
                var b = new i;
                b.controller = this;
                b.body = a;
                b.nextBody = this.m_bodyList;
                b.prevBody = null;
                this.m_bodyList = b;
                if (b.nextBody) b.nextBody.prevBody = b;
                this.m_bodyCount++;
                b.nextController = a.m_controllerList;
                b.prevController = null;
                a.m_controllerList = b;
                if (b.nextController) b.nextController.prevController = b;
                a.m_controllerCount++
            };
            d.prototype.RemoveBody = function (a) {
                for (var b = a.m_controllerList; b && b.controller != this;) b = b.nextController;
                if (b.prevBody) b.prevBody.nextBody = b.nextBody;
                if (b.nextBody) b.nextBody.prevBody = b.prevBody;
                if (b.nextController) b.nextController.prevController = b.prevController;
                if (b.prevController) b.prevController.nextController = b.nextController;
                if (this.m_bodyList == b) this.m_bodyList = b.nextBody;
                if (a.m_controllerList == b) a.m_controllerList = b.nextController;
                a.m_controllerCount--;
                this.m_bodyCount--
            };
            d.prototype.Clear = function () {
                for (; this.m_bodyList;) this.RemoveBody(this.m_bodyList.body)
            };
            d.prototype.GetNext = function () {
                return this.m_next
            };
            d.prototype.GetWorld = function () {
                return this.m_world
            };
            d.prototype.GetBodyList = function () {
                return this.m_bodyList
            };
            i.b2ControllerEdge = function () {};
            a.inherit(r, a.Dynamics.Controllers.b2Controller);
            r.prototype.__super = a.Dynamics.Controllers.b2Controller.prototype;
            r.b2GravityController = function () {
                a.Dynamics.Controllers.b2Controller.b2Controller.apply(this, arguments);
                this.G = 1;
                this.invSqr = !0
            };
            r.prototype.Step = function () {
                var a = null,
                    b = null,
                    c = null,
                    d = 0,
                    f = null,
                    p = null,
                    l = null,
                    m = 0,
                    i = 0,
                    r = 0,
                    m = null;
                if (this.invSqr) for (a = this.m_bodyList; a; a = a.nextBody) {
                    b = a.body;
                    c = b.GetWorldCenter();
                    d = b.GetMass();
                    for (f = this.m_bodyList; f != a; f = f.nextBody) p = f.body, l = p.GetWorldCenter(), m = l.x - c.x, i = l.y - c.y, r = m * m + i * i, r < Number.MIN_VALUE || (m = new g(m, i), m.Multiply(this.G / r / Math.sqrt(r) * d * p.GetMass()), b.IsAwake() && b.ApplyForce(m, c), m.Multiply(-1), p.IsAwake() && p.ApplyForce(m, l))
                } else for (a = this.m_bodyList; a; a = a.nextBody) {
                    b = a.body;
                    c = b.GetWorldCenter();
                    d = b.GetMass();
                    for (f = this.m_bodyList; f != a; f = f.nextBody) p = f.body, l = p.GetWorldCenter(), m = l.x - c.x, i = l.y - c.y, r = m * m + i * i, r < Number.MIN_VALUE || (m = new g(m, i), m.Multiply(this.G / r * d * p.GetMass()), b.IsAwake() && b.ApplyForce(m, c), m.Multiply(-1), p.IsAwake() && p.ApplyForce(m, l))
                }
            };
            a.inherit(s, a.Dynamics.Controllers.b2Controller);
            s.prototype.__super = a.Dynamics.Controllers.b2Controller.prototype;
            s.b2TensorDampingController = function () {
                a.Dynamics.Controllers.b2Controller.b2Controller.apply(this, arguments);
                this.T = new b;
                this.maxTimestep = 0
            };
            s.prototype.SetAxisAligned = function (a, b) {
                a === void 0 && (a = 0);
                b === void 0 && (b = 0);
                this.T.col1.x = -a;
                this.T.col1.y = 0;
                this.T.col2.x = 0;
                this.T.col2.y = -b;
                this.maxTimestep = a > 0 || b > 0 ? 1 / Math.max(a, b) : 0
            };
            s.prototype.Step = function (a) {
                a = a.dt;
                if (!(a <= Number.MIN_VALUE)) {
                    if (a > this.maxTimestep && this.maxTimestep > 0) a = this.maxTimestep;
                    for (var b = this.m_bodyList; b; b = b.nextBody) {
                        var d = b.body;
                        if (d.IsAwake()) {
                            var f = d.GetWorldVector(c.MulMV(this.T, d.GetLocalVector(d.GetLinearVelocity())));
                            d.SetLinearVelocity(new g(d.GetLinearVelocity().x + f.x * a, d.GetLinearVelocity().y + f.y * a))
                        }
                    }
                }
            }
        })();
        (function () {
            var b = a.Common.b2Settings,
                c = a.Common.Math.b2Mat22,
                g = a.Common.Math.b2Mat33,
                p = a.Common.Math.b2Math,
                f = a.Common.Math.b2Vec2,
                l = a.Common.Math.b2Vec3,
                m = a.Dynamics.Joints.b2DistanceJoint,
                d = a.Dynamics.Joints.b2DistanceJointDef,
                i = a.Dynamics.Joints.b2FrictionJoint,
                r = a.Dynamics.Joints.b2FrictionJointDef,
                s = a.Dynamics.Joints.b2GearJoint,
                y = a.Dynamics.Joints.b2GearJointDef,
                I = a.Dynamics.Joints.b2Jacobian,
                w = a.Dynamics.Joints.b2Joint,
                Q = a.Dynamics.Joints.b2JointDef,
                R = a.Dynamics.Joints.b2JointEdge,
                o = a.Dynamics.Joints.b2LineJoint,
                M = a.Dynamics.Joints.b2LineJointDef,
                D = a.Dynamics.Joints.b2MouseJoint,
                N = a.Dynamics.Joints.b2MouseJointDef,
                J = a.Dynamics.Joints.b2PrismaticJoint,
                K = a.Dynamics.Joints.b2PrismaticJointDef,
                C = a.Dynamics.Joints.b2PulleyJoint,
                O = a.Dynamics.Joints.b2PulleyJointDef,
                E = a.Dynamics.Joints.b2RevoluteJoint,
                H = a.Dynamics.Joints.b2RevoluteJointDef,
                P = a.Dynamics.Joints.b2WeldJoint,
                L = a.Dynamics.Joints.b2WeldJointDef;
            a.inherit(m, a.Dynamics.Joints.b2Joint);
            m.prototype.__super = a.Dynamics.Joints.b2Joint.prototype;
            m.b2DistanceJoint = function () {
                a.Dynamics.Joints.b2Joint.b2Joint.apply(this, arguments);
                this.m_localAnchor1 = new f;
                this.m_localAnchor2 = new f;
                this.m_u = new f
            };
            m.prototype.GetAnchorA = function () {
                return this.m_bodyA.GetWorldPoint(this.m_localAnchor1)
            };
            m.prototype.GetAnchorB = function () {
                return this.m_bodyB.GetWorldPoint(this.m_localAnchor2)
            };
            m.prototype.GetReactionForce = function (a) {
                a === void 0 && (a = 0);
                return new f(a * this.m_impulse * this.m_u.x, a * this.m_impulse * this.m_u.y)
            };
            m.prototype.GetReactionTorque = function () {
                return 0
            };
            m.prototype.GetLength = function () {
                return this.m_length
            };
            m.prototype.SetLength = function (a) {
                a === void 0 && (a = 0);
                this.m_length = a
            };
            m.prototype.GetFrequency = function () {
                return this.m_frequencyHz
            };
            m.prototype.SetFrequency = function (a) {
                a === void 0 && (a = 0);
                this.m_frequencyHz = a
            };
            m.prototype.GetDampingRatio = function () {
                return this.m_dampingRatio
            };
            m.prototype.SetDampingRatio = function (a) {
                a === void 0 && (a = 0);
                this.m_dampingRatio = a
            };
            m.prototype.b2DistanceJoint = function (a) {
                this.__super.b2Joint.call(this, a);
                this.m_localAnchor1.SetV(a.localAnchorA);
                this.m_localAnchor2.SetV(a.localAnchorB);
                this.m_length = a.length;
                this.m_frequencyHz = a.frequencyHz;
                this.m_dampingRatio = a.dampingRatio;
                this.m_bias = this.m_gamma = this.m_impulse = 0
            };
            m.prototype.InitVelocityConstraints = function (a) {
                var c, q = 0,
                    h = this.m_bodyA,
                    d = this.m_bodyB;
                c = h.m_xf.R;
                var g = this.m_localAnchor1.x - h.m_sweep.localCenter.x,
                    f = this.m_localAnchor1.y - h.m_sweep.localCenter.y,
                    q = c.col1.x * g + c.col2.x * f,
                    f = c.col1.y * g + c.col2.y * f,
                    g = q;
                c = d.m_xf.R;
                var e = this.m_localAnchor2.x - d.m_sweep.localCenter.x,
                    k = this.m_localAnchor2.y - d.m_sweep.localCenter.y,
                    q = c.col1.x * e + c.col2.x * k,
                    k = c.col1.y * e + c.col2.y * k,
                    e = q;
                this.m_u.x = d.m_sweep.c.x + e - h.m_sweep.c.x - g;
                this.m_u.y = d.m_sweep.c.y + k - h.m_sweep.c.y - f;
                q = Math.sqrt(this.m_u.x * this.m_u.x + this.m_u.y * this.m_u.y);
                q > b.b2_linearSlop ? this.m_u.Multiply(1 / q) : this.m_u.SetZero();
                c = g * this.m_u.y - f * this.m_u.x;
                var p = e * this.m_u.y - k * this.m_u.x;
                c = h.m_invMass + h.m_invI * c * c + d.m_invMass + d.m_invI * p * p;
                this.m_mass = c != 0 ? 1 / c : 0;
                if (this.m_frequencyHz > 0) {
                    q -= this.m_length;
                    var p = 2 * Math.PI * this.m_frequencyHz,
                        l = this.m_mass * p * p;
                    this.m_gamma = a.dt * (2 * this.m_mass * this.m_dampingRatio * p + a.dt * l);
                    this.m_gamma = this.m_gamma != 0 ? 1 / this.m_gamma : 0;
                    this.m_bias = q * a.dt * l * this.m_gamma;
                    this.m_mass = c + this.m_gamma;
                    this.m_mass = this.m_mass != 0 ? 1 / this.m_mass : 0
                }
                a.warmStarting ? (this.m_impulse *= a.dtRatio, a = this.m_impulse * this.m_u.x, c = this.m_impulse * this.m_u.y, h.m_linearVelocity.x -= h.m_invMass * a, h.m_linearVelocity.y -= h.m_invMass * c, h.m_angularVelocity -= h.m_invI * (g * c - f * a), d.m_linearVelocity.x += d.m_invMass * a, d.m_linearVelocity.y += d.m_invMass * c, d.m_angularVelocity += d.m_invI * (e * c - k * a)) : this.m_impulse = 0
            };
            m.prototype.SolveVelocityConstraints = function () {
                var a, b = this.m_bodyA,
                    c = this.m_bodyB;
                a = b.m_xf.R;
                var h = this.m_localAnchor1.x - b.m_sweep.localCenter.x,
                    d = this.m_localAnchor1.y - b.m_sweep.localCenter.y,
                    g = a.col1.x * h + a.col2.x * d,
                    d = a.col1.y * h + a.col2.y * d,
                    h = g;
                a = c.m_xf.R;
                var f = this.m_localAnchor2.x - c.m_sweep.localCenter.x,
                    e = this.m_localAnchor2.y - c.m_sweep.localCenter.y,
                    g = a.col1.x * f + a.col2.x * e,
                    e = a.col1.y * f + a.col2.y * e,
                    f = g,
                    g = -this.m_mass * (this.m_u.x * (c.m_linearVelocity.x + -c.m_angularVelocity * e - (b.m_linearVelocity.x + -b.m_angularVelocity * d)) + this.m_u.y * (c.m_linearVelocity.y + c.m_angularVelocity * f - (b.m_linearVelocity.y + b.m_angularVelocity * h)) + this.m_bias + this.m_gamma * this.m_impulse);
                this.m_impulse += g;
                a = g * this.m_u.x;
                g *= this.m_u.y;
                b.m_linearVelocity.x -= b.m_invMass * a;
                b.m_linearVelocity.y -= b.m_invMass * g;
                b.m_angularVelocity -= b.m_invI * (h * g - d * a);
                c.m_linearVelocity.x += c.m_invMass * a;
                c.m_linearVelocity.y += c.m_invMass * g;
                c.m_angularVelocity += c.m_invI * (f * g - e * a)
            };
            m.prototype.SolvePositionConstraints = function () {
                var a;
                if (this.m_frequencyHz > 0) return !0;
                var c = this.m_bodyA,
                    q = this.m_bodyB;
                a = c.m_xf.R;
                var h = this.m_localAnchor1.x - c.m_sweep.localCenter.x,
                    d = this.m_localAnchor1.y - c.m_sweep.localCenter.y,
                    g = a.col1.x * h + a.col2.x * d,
                    d = a.col1.y * h + a.col2.y * d,
                    h = g;
                a = q.m_xf.R;
                var f = this.m_localAnchor2.x - q.m_sweep.localCenter.x,
                    e = this.m_localAnchor2.y - q.m_sweep.localCenter.y,
                    g = a.col1.x * f + a.col2.x * e,
                    e = a.col1.y * f + a.col2.y * e,
                    f = g,
                    g = q.m_sweep.c.x + f - c.m_sweep.c.x - h,
                    k = q.m_sweep.c.y + e - c.m_sweep.c.y - d;
                a = Math.sqrt(g * g + k * k);
                g /= a;
                k /= a;
                a -= this.m_length;
                a = p.Clamp(a, -b.b2_maxLinearCorrection, b.b2_maxLinearCorrection);
                var l = -this.m_mass * a;
                this.m_u.Set(g, k);
                g = l * this.m_u.x;
                k = l * this.m_u.y;
                c.m_sweep.c.x -= c.m_invMass * g;
                c.m_sweep.c.y -= c.m_invMass * k;
                c.m_sweep.a -= c.m_invI * (h * k - d * g);
                q.m_sweep.c.x += q.m_invMass * g;
                q.m_sweep.c.y += q.m_invMass * k;
                q.m_sweep.a += q.m_invI * (f * k - e * g);
                c.SynchronizeTransform();
                q.SynchronizeTransform();
                return p.Abs(a) < b.b2_linearSlop
            };
            a.inherit(d, a.Dynamics.Joints.b2JointDef);
            d.prototype.__super = a.Dynamics.Joints.b2JointDef.prototype;
            d.b2DistanceJointDef = function () {
                a.Dynamics.Joints.b2JointDef.b2JointDef.apply(this,
                arguments);
                this.localAnchorA = new f;
                this.localAnchorB = new f
            };
            d.prototype.b2DistanceJointDef = function () {
                this.__super.b2JointDef.call(this);
                this.type = w.e_distanceJoint;
                this.length = 1;
                this.dampingRatio = this.frequencyHz = 0
            };
            d.prototype.Initialize = function (a, b, c, h) {
                this.bodyA = a;
                this.bodyB = b;
                this.localAnchorA.SetV(this.bodyA.GetLocalPoint(c));
                this.localAnchorB.SetV(this.bodyB.GetLocalPoint(h));
                a = h.x - c.x;
                c = h.y - c.y;
                this.length = Math.sqrt(a * a + c * c);
                this.dampingRatio = this.frequencyHz = 0
            };
            a.inherit(i, a.Dynamics.Joints.b2Joint);
            i.prototype.__super = a.Dynamics.Joints.b2Joint.prototype;
            i.b2FrictionJoint = function () {
                a.Dynamics.Joints.b2Joint.b2Joint.apply(this, arguments);
                this.m_localAnchorA = new f;
                this.m_localAnchorB = new f;
                this.m_linearMass = new c;
                this.m_linearImpulse = new f
            };
            i.prototype.GetAnchorA = function () {
                return this.m_bodyA.GetWorldPoint(this.m_localAnchorA)
            };
            i.prototype.GetAnchorB = function () {
                return this.m_bodyB.GetWorldPoint(this.m_localAnchorB)
            };
            i.prototype.GetReactionForce = function (a) {
                a === void 0 && (a = 0);
                return new f(a * this.m_linearImpulse.x,
                a * this.m_linearImpulse.y)
            };
            i.prototype.GetReactionTorque = function (a) {
                a === void 0 && (a = 0);
                return a * this.m_angularImpulse
            };
            i.prototype.SetMaxForce = function (a) {
                a === void 0 && (a = 0);
                this.m_maxForce = a
            };
            i.prototype.GetMaxForce = function () {
                return this.m_maxForce
            };
            i.prototype.SetMaxTorque = function (a) {
                a === void 0 && (a = 0);
                this.m_maxTorque = a
            };
            i.prototype.GetMaxTorque = function () {
                return this.m_maxTorque
            };
            i.prototype.b2FrictionJoint = function (a) {
                this.__super.b2Joint.call(this, a);
                this.m_localAnchorA.SetV(a.localAnchorA);
                this.m_localAnchorB.SetV(a.localAnchorB);
                this.m_linearMass.SetZero();
                this.m_angularMass = 0;
                this.m_linearImpulse.SetZero();
                this.m_angularImpulse = 0;
                this.m_maxForce = a.maxForce;
                this.m_maxTorque = a.maxTorque
            };
            i.prototype.InitVelocityConstraints = function (a) {
                var b, q = 0,
                    h = this.m_bodyA,
                    d = this.m_bodyB;
                b = h.m_xf.R;
                var g = this.m_localAnchorA.x - h.m_sweep.localCenter.x,
                    f = this.m_localAnchorA.y - h.m_sweep.localCenter.y,
                    q = b.col1.x * g + b.col2.x * f,
                    f = b.col1.y * g + b.col2.y * f,
                    g = q;
                b = d.m_xf.R;
                var e = this.m_localAnchorB.x - d.m_sweep.localCenter.x,
                    k = this.m_localAnchorB.y - d.m_sweep.localCenter.y,
                    q = b.col1.x * e + b.col2.x * k,
                    k = b.col1.y * e + b.col2.y * k,
                    e = q;
                b = h.m_invMass;
                var q = d.m_invMass,
                    p = h.m_invI,
                    l = d.m_invI,
                    m = new c;
                m.col1.x = b + q;
                m.col2.x = 0;
                m.col1.y = 0;
                m.col2.y = b + q;
                m.col1.x += p * f * f;
                m.col2.x += -p * g * f;
                m.col1.y += -p * g * f;
                m.col2.y += p * g * g;
                m.col1.x += l * k * k;
                m.col2.x += -l * e * k;
                m.col1.y += -l * e * k;
                m.col2.y += l * e * e;
                m.GetInverse(this.m_linearMass);
                this.m_angularMass = p + l;
                if (this.m_angularMass > 0) this.m_angularMass = 1 / this.m_angularMass;
                a.warmStarting ? (this.m_linearImpulse.x *= a.dtRatio, this.m_linearImpulse.y *= a.dtRatio, this.m_angularImpulse *= a.dtRatio, a = this.m_linearImpulse, h.m_linearVelocity.x -= b * a.x, h.m_linearVelocity.y -= b * a.y, h.m_angularVelocity -= p * (g * a.y - f * a.x + this.m_angularImpulse), d.m_linearVelocity.x += q * a.x, d.m_linearVelocity.y += q * a.y, d.m_angularVelocity += l * (e * a.y - k * a.x + this.m_angularImpulse)) : (this.m_linearImpulse.SetZero(), this.m_angularImpulse = 0)
            };
            i.prototype.SolveVelocityConstraints = function (a) {
                var b, c = 0,
                    h = this.m_bodyA,
                    d = this.m_bodyB,
                    g = h.m_linearVelocity,
                    l = h.m_angularVelocity,
                    e = d.m_linearVelocity,
                    k = d.m_angularVelocity,
                    m = h.m_invMass,
                    i = d.m_invMass,
                    o = h.m_invI,
                    r = d.m_invI;
                b = h.m_xf.R;
                var s = this.m_localAnchorA.x - h.m_sweep.localCenter.x,
                    t = this.m_localAnchorA.y - h.m_sweep.localCenter.y,
                    c = b.col1.x * s + b.col2.x * t,
                    t = b.col1.y * s + b.col2.y * t,
                    s = c;
                b = d.m_xf.R;
                var z = this.m_localAnchorB.x - d.m_sweep.localCenter.x,
                    A = this.m_localAnchorB.y - d.m_sweep.localCenter.y,
                    c = b.col1.x * z + b.col2.x * A,
                    A = b.col1.y * z + b.col2.y * A,
                    z = c;
                b = 0;
                var c = -this.m_angularMass * (k - l),
                    B = this.m_angularImpulse;
                b = a.dt * this.m_maxTorque;
                this.m_angularImpulse = p.Clamp(this.m_angularImpulse + c, -b, b);
                c = this.m_angularImpulse - B;
                l -= o * c;
                k += r * c;
                b = p.MulMV(this.m_linearMass, new f(-(e.x - k * A - g.x + l * t), -(e.y + k * z - g.y - l * s)));
                c = this.m_linearImpulse.Copy();
                this.m_linearImpulse.Add(b);
                b = a.dt * this.m_maxForce;
                this.m_linearImpulse.LengthSquared() > b * b && (this.m_linearImpulse.Normalize(), this.m_linearImpulse.Multiply(b));
                b = p.SubtractVV(this.m_linearImpulse, c);
                g.x -= m * b.x;
                g.y -= m * b.y;
                l -= o * (s * b.y - t * b.x);
                e.x += i * b.x;
                e.y += i * b.y;
                k += r * (z * b.y - A * b.x);
                h.m_angularVelocity = l;
                d.m_angularVelocity = k
            };
            i.prototype.SolvePositionConstraints = function () {
                return !0
            };
            a.inherit(r, a.Dynamics.Joints.b2JointDef);
            r.prototype.__super = a.Dynamics.Joints.b2JointDef.prototype;
            r.b2FrictionJointDef = function () {
                a.Dynamics.Joints.b2JointDef.b2JointDef.apply(this, arguments);
                this.localAnchorA = new f;
                this.localAnchorB = new f
            };
            r.prototype.b2FrictionJointDef = function () {
                this.__super.b2JointDef.call(this);
                this.type = w.e_frictionJoint;
                this.maxTorque = this.maxForce = 0
            };
            r.prototype.Initialize = function (a, b, c) {
                this.bodyA = a;
                this.bodyB = b;
                this.localAnchorA.SetV(this.bodyA.GetLocalPoint(c));
                this.localAnchorB.SetV(this.bodyB.GetLocalPoint(c))
            };
            a.inherit(s, a.Dynamics.Joints.b2Joint);
            s.prototype.__super = a.Dynamics.Joints.b2Joint.prototype;
            s.b2GearJoint = function () {
                a.Dynamics.Joints.b2Joint.b2Joint.apply(this, arguments);
                this.m_groundAnchor1 = new f;
                this.m_groundAnchor2 = new f;
                this.m_localAnchor1 = new f;
                this.m_localAnchor2 = new f;
                this.m_J = new I
            };
            s.prototype.GetAnchorA = function () {
                return this.m_bodyA.GetWorldPoint(this.m_localAnchor1)
            };
            s.prototype.GetAnchorB = function () {
                return this.m_bodyB.GetWorldPoint(this.m_localAnchor2)
            };
            s.prototype.GetReactionForce = function (a) {
                a === void 0 && (a = 0);
                return new f(a * this.m_impulse * this.m_J.linearB.x, a * this.m_impulse * this.m_J.linearB.y)
            };
            s.prototype.GetReactionTorque = function (a) {
                a === void 0 && (a = 0);
                var b = this.m_bodyB.m_xf.R,
                    c = this.m_localAnchor1.x - this.m_bodyB.m_sweep.localCenter.x,
                    h = this.m_localAnchor1.y - this.m_bodyB.m_sweep.localCenter.y,
                    d = b.col1.x * c + b.col2.x * h,
                    h = b.col1.y * c + b.col2.y * h;
                return a * (this.m_impulse * this.m_J.angularB - d * this.m_impulse * this.m_J.linearB.y + h * this.m_impulse * this.m_J.linearB.x)
            };
            s.prototype.GetRatio = function () {
                return this.m_ratio
            };
            s.prototype.SetRatio = function (a) {
                a === void 0 && (a = 0);
                this.m_ratio = a
            };
            s.prototype.b2GearJoint = function (a) {
                this.__super.b2Joint.call(this, a);
                var b = parseInt(a.joint1.m_type),
                    c = parseInt(a.joint2.m_type);
                this.m_prismatic2 = this.m_revolute2 = this.m_prismatic1 = this.m_revolute1 = null;
                var h = 0,
                    d = 0;
                this.m_ground1 = a.joint1.GetBodyA();
                this.m_bodyA = a.joint1.GetBodyB();
                b == w.e_revoluteJoint ? (this.m_revolute1 = a.joint1 instanceof E ? a.joint1 : null, this.m_groundAnchor1.SetV(this.m_revolute1.m_localAnchor1), this.m_localAnchor1.SetV(this.m_revolute1.m_localAnchor2), h = this.m_revolute1.GetJointAngle()) : (this.m_prismatic1 = a.joint1 instanceof J ? a.joint1 : null, this.m_groundAnchor1.SetV(this.m_prismatic1.m_localAnchor1), this.m_localAnchor1.SetV(this.m_prismatic1.m_localAnchor2), h = this.m_prismatic1.GetJointTranslation());
                this.m_ground2 = a.joint2.GetBodyA();
                this.m_bodyB = a.joint2.GetBodyB();
                c == w.e_revoluteJoint ? (this.m_revolute2 = a.joint2 instanceof E ? a.joint2 : null, this.m_groundAnchor2.SetV(this.m_revolute2.m_localAnchor1), this.m_localAnchor2.SetV(this.m_revolute2.m_localAnchor2), d = this.m_revolute2.GetJointAngle()) : (this.m_prismatic2 = a.joint2 instanceof J ? a.joint2 : null, this.m_groundAnchor2.SetV(this.m_prismatic2.m_localAnchor1), this.m_localAnchor2.SetV(this.m_prismatic2.m_localAnchor2), d = this.m_prismatic2.GetJointTranslation());
                this.m_ratio = a.ratio;
                this.m_constant = h + this.m_ratio * d;
                this.m_impulse = 0
            };
            s.prototype.InitVelocityConstraints = function (a) {
                var b = this.m_ground1,
                    c = this.m_ground2,
                    h = this.m_bodyA,
                    d = this.m_bodyB,
                    g = 0,
                    f = 0,
                    e = 0,
                    k = 0,
                    p = e = 0,
                    l = 0;
                this.m_J.SetZero();
                this.m_revolute1 ? (this.m_J.angularA = -1, l += h.m_invI) : (b = b.m_xf.R, f = this.m_prismatic1.m_localXAxis1, g = b.col1.x * f.x + b.col2.x * f.y, f = b.col1.y * f.x + b.col2.y * f.y, b = h.m_xf.R, e = this.m_localAnchor1.x - h.m_sweep.localCenter.x, k = this.m_localAnchor1.y - h.m_sweep.localCenter.y, p = b.col1.x * e + b.col2.x * k, k = b.col1.y * e + b.col2.y * k, e = p * f - k * g, this.m_J.linearA.Set(-g, -f), this.m_J.angularA = -e, l += h.m_invMass + h.m_invI * e * e);
                this.m_revolute2 ? (this.m_J.angularB = -this.m_ratio, l += this.m_ratio * this.m_ratio * d.m_invI) : (b = c.m_xf.R, f = this.m_prismatic2.m_localXAxis1, g = b.col1.x * f.x + b.col2.x * f.y, f = b.col1.y * f.x + b.col2.y * f.y, b = d.m_xf.R, e = this.m_localAnchor2.x - d.m_sweep.localCenter.x, k = this.m_localAnchor2.y - d.m_sweep.localCenter.y, p = b.col1.x * e + b.col2.x * k, k = b.col1.y * e + b.col2.y * k, e = p * f - k * g, this.m_J.linearB.Set(-this.m_ratio * g, -this.m_ratio * f), this.m_J.angularB = -this.m_ratio * e, l += this.m_ratio * this.m_ratio * (d.m_invMass + d.m_invI * e * e));
                this.m_mass = l > 0 ? 1 / l : 0;
                a.warmStarting ? (h.m_linearVelocity.x += h.m_invMass * this.m_impulse * this.m_J.linearA.x, h.m_linearVelocity.y += h.m_invMass * this.m_impulse * this.m_J.linearA.y, h.m_angularVelocity += h.m_invI * this.m_impulse * this.m_J.angularA, d.m_linearVelocity.x += d.m_invMass * this.m_impulse * this.m_J.linearB.x, d.m_linearVelocity.y += d.m_invMass * this.m_impulse * this.m_J.linearB.y, d.m_angularVelocity += d.m_invI * this.m_impulse * this.m_J.angularB) : this.m_impulse = 0
            };
            s.prototype.SolveVelocityConstraints = function () {
                var a = this.m_bodyA,
                    b = this.m_bodyB,
                    c = -this.m_mass * this.m_J.Compute(a.m_linearVelocity, a.m_angularVelocity, b.m_linearVelocity, b.m_angularVelocity);
                this.m_impulse += c;
                a.m_linearVelocity.x += a.m_invMass * c * this.m_J.linearA.x;
                a.m_linearVelocity.y += a.m_invMass * c * this.m_J.linearA.y;
                a.m_angularVelocity += a.m_invI * c * this.m_J.angularA;
                b.m_linearVelocity.x += b.m_invMass * c * this.m_J.linearB.x;
                b.m_linearVelocity.y += b.m_invMass * c * this.m_J.linearB.y;
                b.m_angularVelocity += b.m_invI * c * this.m_J.angularB
            };
            s.prototype.SolvePositionConstraints = function () {
                var a = this.m_bodyA,
                    c = this.m_bodyB,
                    q = 0,
                    h = 0,
                    q = this.m_revolute1 ? this.m_revolute1.GetJointAngle() : this.m_prismatic1.GetJointTranslation(),
                    h = this.m_revolute2 ? this.m_revolute2.GetJointAngle() : this.m_prismatic2.GetJointTranslation(),
                    q = -this.m_mass * (this.m_constant - (q + this.m_ratio * h));
                a.m_sweep.c.x += a.m_invMass * q * this.m_J.linearA.x;
                a.m_sweep.c.y += a.m_invMass * q * this.m_J.linearA.y;
                a.m_sweep.a += a.m_invI * q * this.m_J.angularA;
                c.m_sweep.c.x += c.m_invMass * q * this.m_J.linearB.x;
                c.m_sweep.c.y += c.m_invMass * q * this.m_J.linearB.y;
                c.m_sweep.a += c.m_invI * q * this.m_J.angularB;
                a.SynchronizeTransform();
                c.SynchronizeTransform();
                return 0 < b.b2_linearSlop
            };
            a.inherit(y, a.Dynamics.Joints.b2JointDef);
            y.prototype.__super = a.Dynamics.Joints.b2JointDef.prototype;
            y.b2GearJointDef = function () {
                a.Dynamics.Joints.b2JointDef.b2JointDef.apply(this, arguments)
            };
            y.prototype.b2GearJointDef = function () {
                this.__super.b2JointDef.call(this);
                this.type = w.e_gearJoint;
                this.joint2 = this.joint1 = null;
                this.ratio = 1
            };
            I.b2Jacobian = function () {
                this.linearA = new f;
                this.linearB = new f
            };
            I.prototype.SetZero = function () {
                this.linearA.SetZero();
                this.angularA = 0;
                this.linearB.SetZero();
                this.angularB = 0
            };
            I.prototype.Set = function (a, b, c, h) {
                b === void 0 && (b = 0);
                h === void 0 && (h = 0);
                this.linearA.SetV(a);
                this.angularA = b;
                this.linearB.SetV(c);
                this.angularB = h
            };
            I.prototype.Compute = function (a, b, c, h) {
                b === void 0 && (b = 0);
                h === void 0 && (h = 0);
                return this.linearA.x * a.x + this.linearA.y * a.y + this.angularA * b + (this.linearB.x * c.x + this.linearB.y * c.y) + this.angularB * h
            };
            w.b2Joint = function () {
                this.m_edgeA = new R;
                this.m_edgeB = new R;
                this.m_localCenterA = new f;
                this.m_localCenterB = new f
            };
            w.prototype.GetType = function () {
                return this.m_type
            };
            w.prototype.GetAnchorA = function () {
                return null
            };
            w.prototype.GetAnchorB = function () {
                return null
            };
            w.prototype.GetReactionForce = function () {
                return null
            };
            w.prototype.GetReactionTorque = function () {
                return 0
            };
            w.prototype.GetBodyA = function () {
                return this.m_bodyA
            };
            w.prototype.GetBodyB = function () {
                return this.m_bodyB
            };
            w.prototype.GetNext = function () {
                return this.m_next
            };
            w.prototype.GetUserData = function () {
                return this.m_userData
            };
            w.prototype.SetUserData = function (a) {
                this.m_userData = a
            };
            w.prototype.IsActive = function () {
                return this.m_bodyA.IsActive() && this.m_bodyB.IsActive()
            };
            w.Create = function (a) {
                var b = null;
                switch (a.type) {
                    case w.e_distanceJoint:
                        b = new m(a instanceof d ? a : null);
                        break;
                    case w.e_mouseJoint:
                        b = new D(a instanceof N ? a : null);
                        break;
                    case w.e_prismaticJoint:
                        b = new J(a instanceof K ? a : null);
                        break;
                    case w.e_revoluteJoint:
                        b = new E(a instanceof H ? a : null);
                        break;
                    case w.e_pulleyJoint:
                        b = new C(a instanceof
                        O ? a : null);
                        break;
                    case w.e_gearJoint:
                        b = new s(a instanceof y ? a : null);
                        break;
                    case w.e_lineJoint:
                        b = new o(a instanceof M ? a : null);
                        break;
                    case w.e_weldJoint:
                        b = new P(a instanceof L ? a : null);
                        break;
                    case w.e_frictionJoint:
                        b = new i(a instanceof r ? a : null)
                }
                return b
            };
            w.Destroy = function () {};
            w.prototype.b2Joint = function (a) {
                b.b2Assert(a.bodyA != a.bodyB);
                this.m_type = a.type;
                this.m_next = this.m_prev = null;
                this.m_bodyA = a.bodyA;
                this.m_bodyB = a.bodyB;
                this.m_collideConnected = a.collideConnected;
                this.m_islandFlag = !1;
                this.m_userData = a.userData
            };
            w.prototype.InitVelocityConstraints = function () {};
            w.prototype.SolveVelocityConstraints = function () {};
            w.prototype.FinalizeVelocityConstraints = function () {};
            w.prototype.SolvePositionConstraints = function () {
                return !1
            };
            a.postDefs.push(function () {
                a.Dynamics.Joints.b2Joint.e_unknownJoint = 0;
                a.Dynamics.Joints.b2Joint.e_revoluteJoint = 1;
                a.Dynamics.Joints.b2Joint.e_prismaticJoint = 2;
                a.Dynamics.Joints.b2Joint.e_distanceJoint = 3;
                a.Dynamics.Joints.b2Joint.e_pulleyJoint = 4;
                a.Dynamics.Joints.b2Joint.e_mouseJoint = 5;
                a.Dynamics.Joints.b2Joint.e_gearJoint = 6;
                a.Dynamics.Joints.b2Joint.e_lineJoint = 7;
                a.Dynamics.Joints.b2Joint.e_weldJoint = 8;
                a.Dynamics.Joints.b2Joint.e_frictionJoint = 9;
                a.Dynamics.Joints.b2Joint.e_inactiveLimit = 0;
                a.Dynamics.Joints.b2Joint.e_atLowerLimit = 1;
                a.Dynamics.Joints.b2Joint.e_atUpperLimit = 2;
                a.Dynamics.Joints.b2Joint.e_equalLimits = 3
            });
            Q.b2JointDef = function () {};
            Q.prototype.b2JointDef = function () {
                this.type = w.e_unknownJoint;
                this.bodyB = this.bodyA = this.userData = null;
                this.collideConnected = !1
            };
            R.b2JointEdge = function () {};
            a.inherit(o, a.Dynamics.Joints.b2Joint);
            o.prototype.__super = a.Dynamics.Joints.b2Joint.prototype;
            o.b2LineJoint = function () {
                a.Dynamics.Joints.b2Joint.b2Joint.apply(this, arguments);
                this.m_localAnchor1 = new f;
                this.m_localAnchor2 = new f;
                this.m_localXAxis1 = new f;
                this.m_localYAxis1 = new f;
                this.m_axis = new f;
                this.m_perp = new f;
                this.m_K = new c;
                this.m_impulse = new f
            };
            o.prototype.GetAnchorA = function () {
                return this.m_bodyA.GetWorldPoint(this.m_localAnchor1)
            };
            o.prototype.GetAnchorB = function () {
                return this.m_bodyB.GetWorldPoint(this.m_localAnchor2)
            };
            o.prototype.GetReactionForce = function (a) {
                a === void 0 && (a = 0);
                return new f(a * (this.m_impulse.x * this.m_perp.x + (this.m_motorImpulse + this.m_impulse.y) * this.m_axis.x), a * (this.m_impulse.x * this.m_perp.y + (this.m_motorImpulse + this.m_impulse.y) * this.m_axis.y))
            };
            o.prototype.GetReactionTorque = function (a) {
                a === void 0 && (a = 0);
                return a * this.m_impulse.y
            };
            o.prototype.GetJointTranslation = function () {
                var a = this.m_bodyA,
                    b = this.m_bodyB,
                    c = a.GetWorldPoint(this.m_localAnchor1),
                    h = b.GetWorldPoint(this.m_localAnchor2),
                    b = h.x - c.x,
                    c = h.y - c.y,
                    a = a.GetWorldVector(this.m_localXAxis1);
                return a.x * b + a.y * c
            };
            o.prototype.GetJointSpeed = function () {
                var a = this.m_bodyA,
                    b = this.m_bodyB,
                    c;
                c = a.m_xf.R;
                var h = this.m_localAnchor1.x - a.m_sweep.localCenter.x,
                    d = this.m_localAnchor1.y - a.m_sweep.localCenter.y,
                    g = c.col1.x * h + c.col2.x * d,
                    d = c.col1.y * h + c.col2.y * d,
                    h = g;
                c = b.m_xf.R;
                var f = this.m_localAnchor2.x - b.m_sweep.localCenter.x,
                    e = this.m_localAnchor2.y - b.m_sweep.localCenter.y,
                    g = c.col1.x * f + c.col2.x * e,
                    e = c.col1.y * f + c.col2.y * e,
                    f = g;
                c = b.m_sweep.c.x + f - (a.m_sweep.c.x + h);
                var g = b.m_sweep.c.y + e - (a.m_sweep.c.y + d),
                    k = a.GetWorldVector(this.m_localXAxis1),
                    p = a.m_linearVelocity,
                    l = b.m_linearVelocity,
                    a = a.m_angularVelocity,
                    b = b.m_angularVelocity;
                return c * -a * k.y + g * a * k.x + (k.x * (l.x + -b * e - p.x - -a * d) + k.y * (l.y + b * f - p.y - a * h))
            };
            o.prototype.IsLimitEnabled = function () {
                return this.m_enableLimit
            };
            o.prototype.EnableLimit = function (a) {
                this.m_bodyA.SetAwake(!0);
                this.m_bodyB.SetAwake(!0);
                this.m_enableLimit = a
            };
            o.prototype.GetLowerLimit = function () {
                return this.m_lowerTranslation
            };
            o.prototype.GetUpperLimit = function () {
                return this.m_upperTranslation
            };
            o.prototype.SetLimits = function (a, b) {
                a === void 0 && (a = 0);
                b === void 0 && (b = 0);
                this.m_bodyA.SetAwake(!0);
                this.m_bodyB.SetAwake(!0);
                this.m_lowerTranslation = a;
                this.m_upperTranslation = b
            };
            o.prototype.IsMotorEnabled = function () {
                return this.m_enableMotor
            };
            o.prototype.EnableMotor = function (a) {
                this.m_bodyA.SetAwake(!0);
                this.m_bodyB.SetAwake(!0);
                this.m_enableMotor = a
            };
            o.prototype.SetMotorSpeed = function (a) {
                a === void 0 && (a = 0);
                this.m_bodyA.SetAwake(!0);
                this.m_bodyB.SetAwake(!0);
                this.m_motorSpeed = a
            };
            o.prototype.GetMotorSpeed = function () {
                return this.m_motorSpeed
            };
            o.prototype.SetMaxMotorForce = function (a) {
                a === void 0 && (a = 0);
                this.m_bodyA.SetAwake(!0);
                this.m_bodyB.SetAwake(!0);
                this.m_maxMotorForce = a
            };
            o.prototype.GetMaxMotorForce = function () {
                return this.m_maxMotorForce
            };
            o.prototype.GetMotorForce = function () {
                return this.m_motorImpulse
            };
            o.prototype.b2LineJoint = function (a) {
                this.__super.b2Joint.call(this, a);
                this.m_localAnchor1.SetV(a.localAnchorA);
                this.m_localAnchor2.SetV(a.localAnchorB);
                this.m_localXAxis1.SetV(a.localAxisA);
                this.m_localYAxis1.x = -this.m_localXAxis1.y;
                this.m_localYAxis1.y = this.m_localXAxis1.x;
                this.m_impulse.SetZero();
                this.m_motorImpulse = this.m_motorMass = 0;
                this.m_lowerTranslation = a.lowerTranslation;
                this.m_upperTranslation = a.upperTranslation;
                this.m_maxMotorForce = a.maxMotorForce;
                this.m_motorSpeed = a.motorSpeed;
                this.m_enableLimit = a.enableLimit;
                this.m_enableMotor = a.enableMotor;
                this.m_limitState = w.e_inactiveLimit;
                this.m_axis.SetZero();
                this.m_perp.SetZero()
            };
            o.prototype.InitVelocityConstraints = function (a) {
                var c = this.m_bodyA,
                    d = this.m_bodyB,
                    h, g = 0;
                this.m_localCenterA.SetV(c.GetLocalCenter());
                this.m_localCenterB.SetV(d.GetLocalCenter());
                var f = c.GetTransform();
                d.GetTransform();
                h = c.m_xf.R;
                var l = this.m_localAnchor1.x - this.m_localCenterA.x,
                    e = this.m_localAnchor1.y - this.m_localCenterA.y,
                    g = h.col1.x * l + h.col2.x * e,
                    e = h.col1.y * l + h.col2.y * e,
                    l = g;
                h = d.m_xf.R;
                var k = this.m_localAnchor2.x - this.m_localCenterB.x,
                    m = this.m_localAnchor2.y - this.m_localCenterB.y,
                    g = h.col1.x * k + h.col2.x * m,
                    m = h.col1.y * k + h.col2.y * m,
                    k = g;
                h = d.m_sweep.c.x + k - c.m_sweep.c.x - l;
                g = d.m_sweep.c.y + m - c.m_sweep.c.y - e;
                this.m_invMassA = c.m_invMass;
                this.m_invMassB = d.m_invMass;
                this.m_invIA = c.m_invI;
                this.m_invIB = d.m_invI;
                this.m_axis.SetV(p.MulMV(f.R, this.m_localXAxis1));
                this.m_a1 = (h + l) * this.m_axis.y - (g + e) * this.m_axis.x;
                this.m_a2 = k * this.m_axis.y - m * this.m_axis.x;
                this.m_motorMass = this.m_invMassA + this.m_invMassB + this.m_invIA * this.m_a1 * this.m_a1 + this.m_invIB * this.m_a2 * this.m_a2;
                this.m_motorMass = this.m_motorMass > Number.MIN_VALUE ? 1 / this.m_motorMass : 0;
                this.m_perp.SetV(p.MulMV(f.R,
                this.m_localYAxis1));
                this.m_s1 = (h + l) * this.m_perp.y - (g + e) * this.m_perp.x;
                this.m_s2 = k * this.m_perp.y - m * this.m_perp.x;
                f = this.m_invMassA;
                l = this.m_invMassB;
                e = this.m_invIA;
                k = this.m_invIB;
                this.m_K.col1.x = f + l + e * this.m_s1 * this.m_s1 + k * this.m_s2 * this.m_s2;
                this.m_K.col1.y = e * this.m_s1 * this.m_a1 + k * this.m_s2 * this.m_a2;
                this.m_K.col2.x = this.m_K.col1.y;
                this.m_K.col2.y = f + l + e * this.m_a1 * this.m_a1 + k * this.m_a2 * this.m_a2;
                if (this.m_enableLimit) if (h = this.m_axis.x * h + this.m_axis.y * g, p.Abs(this.m_upperTranslation - this.m_lowerTranslation) < 2 * b.b2_linearSlop) this.m_limitState = w.e_equalLimits;
                else if (h <= this.m_lowerTranslation) {
                    if (this.m_limitState != w.e_atLowerLimit) this.m_limitState = w.e_atLowerLimit, this.m_impulse.y = 0
                } else if (h >= this.m_upperTranslation) {
                    if (this.m_limitState != w.e_atUpperLimit) this.m_limitState = w.e_atUpperLimit, this.m_impulse.y = 0
                } else this.m_limitState = w.e_inactiveLimit, this.m_impulse.y = 0;
                else this.m_limitState = w.e_inactiveLimit;
                if (this.m_enableMotor == !1) this.m_motorImpulse = 0;
                a.warmStarting ? (this.m_impulse.x *= a.dtRatio,
                this.m_impulse.y *= a.dtRatio, this.m_motorImpulse *= a.dtRatio, a = this.m_impulse.x * this.m_perp.x + (this.m_motorImpulse + this.m_impulse.y) * this.m_axis.x, h = this.m_impulse.x * this.m_perp.y + (this.m_motorImpulse + this.m_impulse.y) * this.m_axis.y, g = this.m_impulse.x * this.m_s1 + (this.m_motorImpulse + this.m_impulse.y) * this.m_a1, f = this.m_impulse.x * this.m_s2 + (this.m_motorImpulse + this.m_impulse.y) * this.m_a2, c.m_linearVelocity.x -= this.m_invMassA * a, c.m_linearVelocity.y -= this.m_invMassA * h, c.m_angularVelocity -= this.m_invIA * g, d.m_linearVelocity.x += this.m_invMassB * a, d.m_linearVelocity.y += this.m_invMassB * h, d.m_angularVelocity += this.m_invIB * f) : (this.m_impulse.SetZero(), this.m_motorImpulse = 0)
            };
            o.prototype.SolveVelocityConstraints = function (a) {
                var b = this.m_bodyA,
                    c = this.m_bodyB,
                    h = b.m_linearVelocity,
                    d = b.m_angularVelocity,
                    g = c.m_linearVelocity,
                    l = c.m_angularVelocity,
                    e = 0,
                    k = 0,
                    m = 0,
                    i = 0;
                if (this.m_enableMotor && this.m_limitState != w.e_equalLimits) i = this.m_motorMass * (this.m_motorSpeed - (this.m_axis.x * (g.x - h.x) + this.m_axis.y * (g.y - h.y) + this.m_a2 * l - this.m_a1 * d)), e = this.m_motorImpulse, k = a.dt * this.m_maxMotorForce, this.m_motorImpulse = p.Clamp(this.m_motorImpulse + i, -k, k), i = this.m_motorImpulse - e, e = i * this.m_axis.x, k = i * this.m_axis.y, m = i * this.m_a1, i *= this.m_a2, h.x -= this.m_invMassA * e, h.y -= this.m_invMassA * k, d -= this.m_invIA * m, g.x += this.m_invMassB * e, g.y += this.m_invMassB * k, l += this.m_invIB * i;
                k = this.m_perp.x * (g.x - h.x) + this.m_perp.y * (g.y - h.y) + this.m_s2 * l - this.m_s1 * d;
                if (this.m_enableLimit && this.m_limitState != w.e_inactiveLimit) {
                    m = this.m_axis.x * (g.x - h.x) + this.m_axis.y * (g.y - h.y) + this.m_a2 * l - this.m_a1 * d;
                    e = this.m_impulse.Copy();
                    a = this.m_K.Solve(new f, -k, -m);
                    this.m_impulse.Add(a);
                    if (this.m_limitState == w.e_atLowerLimit) this.m_impulse.y = p.Max(this.m_impulse.y, 0);
                    else if (this.m_limitState == w.e_atUpperLimit) this.m_impulse.y = p.Min(this.m_impulse.y, 0);
                    k = -k - (this.m_impulse.y - e.y) * this.m_K.col2.x;
                    m = 0;
                    m = this.m_K.col1.x != 0 ? k / this.m_K.col1.x + e.x : e.x;
                    this.m_impulse.x = m;
                    a.x = this.m_impulse.x - e.x;
                    a.y = this.m_impulse.y - e.y;
                    e = a.x * this.m_perp.x + a.y * this.m_axis.x;
                    k = a.x * this.m_perp.y + a.y * this.m_axis.y;
                    m = a.x * this.m_s1 + a.y * this.m_a1;
                    i = a.x * this.m_s2 + a.y * this.m_a2
                } else a = 0, a = this.m_K.col1.x != 0 ? -k / this.m_K.col1.x : 0, this.m_impulse.x += a, e = a * this.m_perp.x, k = a * this.m_perp.y, m = a * this.m_s1, i = a * this.m_s2;
                h.x -= this.m_invMassA * e;
                h.y -= this.m_invMassA * k;
                d -= this.m_invIA * m;
                g.x += this.m_invMassB * e;
                g.y += this.m_invMassB * k;
                l += this.m_invIB * i;
                b.m_linearVelocity.SetV(h);
                b.m_angularVelocity = d;
                c.m_linearVelocity.SetV(g);
                c.m_angularVelocity = l
            };
            o.prototype.SolvePositionConstraints = function () {
                var a = this.m_bodyA,
                    n = this.m_bodyB,
                    d = a.m_sweep.c,
                    h = a.m_sweep.a,
                    g = n.m_sweep.c,
                    l = n.m_sweep.a,
                    m, e = 0,
                    k = 0,
                    i = 0,
                    o = 0,
                    r = m = 0,
                    s = 0,
                    k = !1,
                    t = 0,
                    z = c.FromAngle(h),
                    i = c.FromAngle(l);
                m = z;
                var s = this.m_localAnchor1.x - this.m_localCenterA.x,
                    A = this.m_localAnchor1.y - this.m_localCenterA.y,
                    e = m.col1.x * s + m.col2.x * A,
                    A = m.col1.y * s + m.col2.y * A,
                    s = e;
                m = i;
                i = this.m_localAnchor2.x - this.m_localCenterB.x;
                o = this.m_localAnchor2.y - this.m_localCenterB.y;
                e = m.col1.x * i + m.col2.x * o;
                o = m.col1.y * i + m.col2.y * o;
                i = e;
                m = g.x + i - d.x - s;
                e = g.y + o - d.y - A;
                if (this.m_enableLimit) {
                    this.m_axis = p.MulMV(z, this.m_localXAxis1);
                    this.m_a1 = (m + s) * this.m_axis.y - (e + A) * this.m_axis.x;
                    this.m_a2 = i * this.m_axis.y - o * this.m_axis.x;
                    var B = this.m_axis.x * m + this.m_axis.y * e;
                    p.Abs(this.m_upperTranslation - this.m_lowerTranslation) < 2 * b.b2_linearSlop ? (t = p.Clamp(B, -b.b2_maxLinearCorrection, b.b2_maxLinearCorrection), r = p.Abs(B), k = !0) : B <= this.m_lowerTranslation ? (t = p.Clamp(B - this.m_lowerTranslation + b.b2_linearSlop, -b.b2_maxLinearCorrection, 0), r = this.m_lowerTranslation - B, k = !0) : B >= this.m_upperTranslation && (t = p.Clamp(B - this.m_upperTranslation + b.b2_linearSlop, 0, b.b2_maxLinearCorrection), r = B - this.m_upperTranslation, k = !0)
                }
                this.m_perp = p.MulMV(z, this.m_localYAxis1);
                this.m_s1 = (m + s) * this.m_perp.y - (e + A) * this.m_perp.x;
                this.m_s2 = i * this.m_perp.y - o * this.m_perp.x;
                z = new f;
                A = this.m_perp.x * m + this.m_perp.y * e;
                r = p.Max(r, p.Abs(A));
                s = 0;
                k ? (k = this.m_invMassA, i = this.m_invMassB, o = this.m_invIA, m = this.m_invIB, this.m_K.col1.x = k + i + o * this.m_s1 * this.m_s1 + m * this.m_s2 * this.m_s2, this.m_K.col1.y = o * this.m_s1 * this.m_a1 + m * this.m_s2 * this.m_a2, this.m_K.col2.x = this.m_K.col1.y,
                this.m_K.col2.y = k + i + o * this.m_a1 * this.m_a1 + m * this.m_a2 * this.m_a2, this.m_K.Solve(z, -A, -t)) : (k = this.m_invMassA, i = this.m_invMassB, o = this.m_invIA, m = this.m_invIB, t = k + i + o * this.m_s1 * this.m_s1 + m * this.m_s2 * this.m_s2, k = 0, z.x = t != 0 ? -A / t : 0, z.y = 0);
                t = z.x * this.m_perp.x + z.y * this.m_axis.x;
                k = z.x * this.m_perp.y + z.y * this.m_axis.y;
                A = z.x * this.m_s1 + z.y * this.m_a1;
                z = z.x * this.m_s2 + z.y * this.m_a2;
                d.x -= this.m_invMassA * t;
                d.y -= this.m_invMassA * k;
                h -= this.m_invIA * A;
                g.x += this.m_invMassB * t;
                g.y += this.m_invMassB * k;
                l += this.m_invIB * z;
                a.m_sweep.a = h;
                n.m_sweep.a = l;
                a.SynchronizeTransform();
                n.SynchronizeTransform();
                return r <= b.b2_linearSlop && s <= b.b2_angularSlop
            };
            a.inherit(M, a.Dynamics.Joints.b2JointDef);
            M.prototype.__super = a.Dynamics.Joints.b2JointDef.prototype;
            M.b2LineJointDef = function () {
                a.Dynamics.Joints.b2JointDef.b2JointDef.apply(this, arguments);
                this.localAnchorA = new f;
                this.localAnchorB = new f;
                this.localAxisA = new f
            };
            M.prototype.b2LineJointDef = function () {
                this.__super.b2JointDef.call(this);
                this.type = w.e_lineJoint;
                this.localAxisA.Set(1, 0);
                this.enableLimit = !1;
                this.upperTranslation = this.lowerTranslation = 0;
                this.enableMotor = !1;
                this.motorSpeed = this.maxMotorForce = 0
            };
            M.prototype.Initialize = function (a, b, c, h) {
                this.bodyA = a;
                this.bodyB = b;
                this.localAnchorA = this.bodyA.GetLocalPoint(c);
                this.localAnchorB = this.bodyB.GetLocalPoint(c);
                this.localAxisA = this.bodyA.GetLocalVector(h)
            };
            a.inherit(D, a.Dynamics.Joints.b2Joint);
            D.prototype.__super = a.Dynamics.Joints.b2Joint.prototype;
            D.b2MouseJoint = function () {
                a.Dynamics.Joints.b2Joint.b2Joint.apply(this, arguments);
                this.K = new c;
                this.K1 = new c;
                this.K2 = new c;
                this.m_localAnchor = new f;
                this.m_target = new f;
                this.m_impulse = new f;
                this.m_mass = new c;
                this.m_C = new f
            };
            D.prototype.GetAnchorA = function () {
                return this.m_target
            };
            D.prototype.GetAnchorB = function () {
                return this.m_bodyB.GetWorldPoint(this.m_localAnchor)
            };
            D.prototype.GetReactionForce = function (a) {
                a === void 0 && (a = 0);
                return new f(a * this.m_impulse.x, a * this.m_impulse.y)
            };
            D.prototype.GetReactionTorque = function () {
                return 0
            };
            D.prototype.GetTarget = function () {
                return this.m_target
            };
            D.prototype.SetTarget = function (a) {
                this.m_bodyB.IsAwake() == !1 && this.m_bodyB.SetAwake(!0);
                this.m_target = a
            };
            D.prototype.GetMaxForce = function () {
                return this.m_maxForce
            };
            D.prototype.SetMaxForce = function (a) {
                a === void 0 && (a = 0);
                this.m_maxForce = a
            };
            D.prototype.GetFrequency = function () {
                return this.m_frequencyHz
            };
            D.prototype.SetFrequency = function (a) {
                a === void 0 && (a = 0);
                this.m_frequencyHz = a
            };
            D.prototype.GetDampingRatio = function () {
                return this.m_dampingRatio
            };
            D.prototype.SetDampingRatio = function (a) {
                a === void 0 && (a = 0);
                this.m_dampingRatio = a
            };
            D.prototype.b2MouseJoint = function (a) {
                this.__super.b2Joint.call(this, a);
                this.m_target.SetV(a.target);
                var b = this.m_target.x - this.m_bodyB.m_xf.position.x,
                    c = this.m_target.y - this.m_bodyB.m_xf.position.y,
                    h = this.m_bodyB.m_xf.R;
                this.m_localAnchor.x = b * h.col1.x + c * h.col1.y;
                this.m_localAnchor.y = b * h.col2.x + c * h.col2.y;
                this.m_maxForce = a.maxForce;
                this.m_impulse.SetZero();
                this.m_frequencyHz = a.frequencyHz;
                this.m_dampingRatio = a.dampingRatio;
                this.m_gamma = this.m_beta = 0
            };
            D.prototype.InitVelocityConstraints = function (a) {
                var b = this.m_bodyB,
                    c = b.GetMass(),
                    h = 2 * Math.PI * this.m_frequencyHz,
                    d = c * h * h;
                this.m_gamma = a.dt * (2 * c * this.m_dampingRatio * h + a.dt * d);
                this.m_gamma = this.m_gamma != 0 ? 1 / this.m_gamma : 0;
                this.m_beta = a.dt * d * this.m_gamma;
                var d = b.m_xf.R,
                    c = this.m_localAnchor.x - b.m_sweep.localCenter.x,
                    h = this.m_localAnchor.y - b.m_sweep.localCenter.y,
                    g = d.col1.x * c + d.col2.x * h,
                    h = d.col1.y * c + d.col2.y * h,
                    c = g,
                    d = b.m_invMass,
                    g = b.m_invI;
                this.K1.col1.x = d;
                this.K1.col2.x = 0;
                this.K1.col1.y = 0;
                this.K1.col2.y = d;
                this.K2.col1.x = g * h * h;
                this.K2.col2.x = -g * c * h;
                this.K2.col1.y = -g * c * h;
                this.K2.col2.y = g * c * c;
                this.K.SetM(this.K1);
                this.K.AddM(this.K2);
                this.K.col1.x += this.m_gamma;
                this.K.col2.y += this.m_gamma;
                this.K.GetInverse(this.m_mass);
                this.m_C.x = b.m_sweep.c.x + c - this.m_target.x;
                this.m_C.y = b.m_sweep.c.y + h - this.m_target.y;
                b.m_angularVelocity *= 0.98;
                this.m_impulse.x *= a.dtRatio;
                this.m_impulse.y *= a.dtRatio;
                b.m_linearVelocity.x += d * this.m_impulse.x;
                b.m_linearVelocity.y += d * this.m_impulse.y;
                b.m_angularVelocity += g * (c * this.m_impulse.y - h * this.m_impulse.x)
            };
            D.prototype.SolveVelocityConstraints = function (a) {
                var b = this.m_bodyB,
                    c, h = 0,
                    d = 0;
                c = b.m_xf.R;
                var g = this.m_localAnchor.x - b.m_sweep.localCenter.x,
                    f = this.m_localAnchor.y - b.m_sweep.localCenter.y,
                    h = c.col1.x * g + c.col2.x * f,
                    f = c.col1.y * g + c.col2.y * f,
                    g = h,
                    h = b.m_linearVelocity.x + -b.m_angularVelocity * f,
                    e = b.m_linearVelocity.y + b.m_angularVelocity * g;
                c = this.m_mass;
                h = h + this.m_beta * this.m_C.x + this.m_gamma * this.m_impulse.x;
                d = e + this.m_beta * this.m_C.y + this.m_gamma * this.m_impulse.y;
                e = -(c.col1.x * h + c.col2.x * d);
                d = -(c.col1.y * h + c.col2.y * d);
                c = this.m_impulse.x;
                h = this.m_impulse.y;
                this.m_impulse.x += e;
                this.m_impulse.y += d;
                a = a.dt * this.m_maxForce;
                this.m_impulse.LengthSquared() > a * a && this.m_impulse.Multiply(a / this.m_impulse.Length());
                e = this.m_impulse.x - c;
                d = this.m_impulse.y - h;
                b.m_linearVelocity.x += b.m_invMass * e;
                b.m_linearVelocity.y += b.m_invMass * d;
                b.m_angularVelocity += b.m_invI * (g * d - f * e)
            };
            D.prototype.SolvePositionConstraints = function () {
                return !0
            };
            a.inherit(N, a.Dynamics.Joints.b2JointDef);
            N.prototype.__super = a.Dynamics.Joints.b2JointDef.prototype;
            N.b2MouseJointDef = function () {
                a.Dynamics.Joints.b2JointDef.b2JointDef.apply(this,
                arguments);
                this.target = new f
            };
            N.prototype.b2MouseJointDef = function () {
                this.__super.b2JointDef.call(this);
                this.type = w.e_mouseJoint;
                this.maxForce = 0;
                this.frequencyHz = 5;
                this.dampingRatio = 0.7
            };
            a.inherit(J, a.Dynamics.Joints.b2Joint);
            J.prototype.__super = a.Dynamics.Joints.b2Joint.prototype;
            J.b2PrismaticJoint = function () {
                a.Dynamics.Joints.b2Joint.b2Joint.apply(this, arguments);
                this.m_localAnchor1 = new f;
                this.m_localAnchor2 = new f;
                this.m_localXAxis1 = new f;
                this.m_localYAxis1 = new f;
                this.m_axis = new f;
                this.m_perp = new f;
                this.m_K = new g;
                this.m_impulse = new l
            };
            J.prototype.GetAnchorA = function () {
                return this.m_bodyA.GetWorldPoint(this.m_localAnchor1)
            };
            J.prototype.GetAnchorB = function () {
                return this.m_bodyB.GetWorldPoint(this.m_localAnchor2)
            };
            J.prototype.GetReactionForce = function (a) {
                a === void 0 && (a = 0);
                return new f(a * (this.m_impulse.x * this.m_perp.x + (this.m_motorImpulse + this.m_impulse.z) * this.m_axis.x), a * (this.m_impulse.x * this.m_perp.y + (this.m_motorImpulse + this.m_impulse.z) * this.m_axis.y))
            };
            J.prototype.GetReactionTorque = function (a) {
                a === void 0 && (a = 0);
                return a * this.m_impulse.y
            };
            J.prototype.GetJointTranslation = function () {
                var a = this.m_bodyA,
                    b = this.m_bodyB,
                    c = a.GetWorldPoint(this.m_localAnchor1),
                    h = b.GetWorldPoint(this.m_localAnchor2),
                    b = h.x - c.x,
                    c = h.y - c.y,
                    a = a.GetWorldVector(this.m_localXAxis1);
                return a.x * b + a.y * c
            };
            J.prototype.GetJointSpeed = function () {
                var a = this.m_bodyA,
                    b = this.m_bodyB,
                    c;
                c = a.m_xf.R;
                var h = this.m_localAnchor1.x - a.m_sweep.localCenter.x,
                    d = this.m_localAnchor1.y - a.m_sweep.localCenter.y,
                    g = c.col1.x * h + c.col2.x * d,
                    d = c.col1.y * h + c.col2.y * d,
                    h = g;
                c = b.m_xf.R;
                var f = this.m_localAnchor2.x - b.m_sweep.localCenter.x,
                    e = this.m_localAnchor2.y - b.m_sweep.localCenter.y,
                    g = c.col1.x * f + c.col2.x * e,
                    e = c.col1.y * f + c.col2.y * e,
                    f = g;
                c = b.m_sweep.c.x + f - (a.m_sweep.c.x + h);
                var g = b.m_sweep.c.y + e - (a.m_sweep.c.y + d),
                    k = a.GetWorldVector(this.m_localXAxis1),
                    m = a.m_linearVelocity,
                    l = b.m_linearVelocity,
                    a = a.m_angularVelocity,
                    b = b.m_angularVelocity;
                return c * -a * k.y + g * a * k.x + (k.x * (l.x + -b * e - m.x - -a * d) + k.y * (l.y + b * f - m.y - a * h))
            };
            J.prototype.IsLimitEnabled = function () {
                return this.m_enableLimit
            };
            J.prototype.EnableLimit = function (a) {
                this.m_bodyA.SetAwake(!0);
                this.m_bodyB.SetAwake(!0);
                this.m_enableLimit = a
            };
            J.prototype.GetLowerLimit = function () {
                return this.m_lowerTranslation
            };
            J.prototype.GetUpperLimit = function () {
                return this.m_upperTranslation
            };
            J.prototype.SetLimits = function (a, b) {
                a === void 0 && (a = 0);
                b === void 0 && (b = 0);
                this.m_bodyA.SetAwake(!0);
                this.m_bodyB.SetAwake(!0);
                this.m_lowerTranslation = a;
                this.m_upperTranslation = b
            };
            J.prototype.IsMotorEnabled = function () {
                return this.m_enableMotor
            };
            J.prototype.EnableMotor = function (a) {
                this.m_bodyA.SetAwake(!0);
                this.m_bodyB.SetAwake(!0);
                this.m_enableMotor = a
            };
            J.prototype.SetMotorSpeed = function (a) {
                a === void 0 && (a = 0);
                this.m_bodyA.SetAwake(!0);
                this.m_bodyB.SetAwake(!0);
                this.m_motorSpeed = a
            };
            J.prototype.GetMotorSpeed = function () {
                return this.m_motorSpeed
            };
            J.prototype.SetMaxMotorForce = function (a) {
                a === void 0 && (a = 0);
                this.m_bodyA.SetAwake(!0);
                this.m_bodyB.SetAwake(!0);
                this.m_maxMotorForce = a
            };
            J.prototype.GetMotorForce = function () {
                return this.m_motorImpulse
            };
            J.prototype.b2PrismaticJoint = function (a) {
                this.__super.b2Joint.call(this, a);
                this.m_localAnchor1.SetV(a.localAnchorA);
                this.m_localAnchor2.SetV(a.localAnchorB);
                this.m_localXAxis1.SetV(a.localAxisA);
                this.m_localYAxis1.x = -this.m_localXAxis1.y;
                this.m_localYAxis1.y = this.m_localXAxis1.x;
                this.m_refAngle = a.referenceAngle;
                this.m_impulse.SetZero();
                this.m_motorImpulse = this.m_motorMass = 0;
                this.m_lowerTranslation = a.lowerTranslation;
                this.m_upperTranslation = a.upperTranslation;
                this.m_maxMotorForce = a.maxMotorForce;
                this.m_motorSpeed = a.motorSpeed;
                this.m_enableLimit = a.enableLimit;
                this.m_enableMotor = a.enableMotor;
                this.m_limitState = w.e_inactiveLimit;
                this.m_axis.SetZero();
                this.m_perp.SetZero()
            };
            J.prototype.InitVelocityConstraints = function (a) {
                var c = this.m_bodyA,
                    d = this.m_bodyB,
                    h, g = 0;
                this.m_localCenterA.SetV(c.GetLocalCenter());
                this.m_localCenterB.SetV(d.GetLocalCenter());
                var f = c.GetTransform();
                d.GetTransform();
                h = c.m_xf.R;
                var m = this.m_localAnchor1.x - this.m_localCenterA.x,
                    e = this.m_localAnchor1.y - this.m_localCenterA.y,
                    g = h.col1.x * m + h.col2.x * e,
                    e = h.col1.y * m + h.col2.y * e,
                    m = g;
                h = d.m_xf.R;
                var k = this.m_localAnchor2.x - this.m_localCenterB.x,
                    l = this.m_localAnchor2.y - this.m_localCenterB.y,
                    g = h.col1.x * k + h.col2.x * l,
                    l = h.col1.y * k + h.col2.y * l,
                    k = g;
                h = d.m_sweep.c.x + k - c.m_sweep.c.x - m;
                g = d.m_sweep.c.y + l - c.m_sweep.c.y - e;
                this.m_invMassA = c.m_invMass;
                this.m_invMassB = d.m_invMass;
                this.m_invIA = c.m_invI;
                this.m_invIB = d.m_invI;
                this.m_axis.SetV(p.MulMV(f.R, this.m_localXAxis1));
                this.m_a1 = (h + m) * this.m_axis.y - (g + e) * this.m_axis.x;
                this.m_a2 = k * this.m_axis.y - l * this.m_axis.x;
                this.m_motorMass = this.m_invMassA + this.m_invMassB + this.m_invIA * this.m_a1 * this.m_a1 + this.m_invIB * this.m_a2 * this.m_a2;
                if (this.m_motorMass > Number.MIN_VALUE) this.m_motorMass = 1 / this.m_motorMass;
                this.m_perp.SetV(p.MulMV(f.R, this.m_localYAxis1));
                this.m_s1 = (h + m) * this.m_perp.y - (g + e) * this.m_perp.x;
                this.m_s2 = k * this.m_perp.y - l * this.m_perp.x;
                f = this.m_invMassA;
                m = this.m_invMassB;
                e = this.m_invIA;
                k = this.m_invIB;
                this.m_K.col1.x = f + m + e * this.m_s1 * this.m_s1 + k * this.m_s2 * this.m_s2;
                this.m_K.col1.y = e * this.m_s1 + k * this.m_s2;
                this.m_K.col1.z = e * this.m_s1 * this.m_a1 + k * this.m_s2 * this.m_a2;
                this.m_K.col2.x = this.m_K.col1.y;
                this.m_K.col2.y = e + k;
                this.m_K.col2.z = e * this.m_a1 + k * this.m_a2;
                this.m_K.col3.x = this.m_K.col1.z;
                this.m_K.col3.y = this.m_K.col2.z;
                this.m_K.col3.z = f + m + e * this.m_a1 * this.m_a1 + k * this.m_a2 * this.m_a2;
                if (this.m_enableLimit) if (h = this.m_axis.x * h + this.m_axis.y * g, p.Abs(this.m_upperTranslation - this.m_lowerTranslation) < 2 * b.b2_linearSlop) this.m_limitState = w.e_equalLimits;
                else if (h <= this.m_lowerTranslation) {
                    if (this.m_limitState != w.e_atLowerLimit) this.m_limitState = w.e_atLowerLimit, this.m_impulse.z = 0
                } else if (h >= this.m_upperTranslation) {
                    if (this.m_limitState != w.e_atUpperLimit) this.m_limitState = w.e_atUpperLimit, this.m_impulse.z = 0
                } else this.m_limitState = w.e_inactiveLimit, this.m_impulse.z = 0;
                else this.m_limitState = w.e_inactiveLimit;
                if (this.m_enableMotor == !1) this.m_motorImpulse = 0;
                a.warmStarting ? (this.m_impulse.x *= a.dtRatio, this.m_impulse.y *= a.dtRatio, this.m_motorImpulse *= a.dtRatio, a = this.m_impulse.x * this.m_perp.x + (this.m_motorImpulse + this.m_impulse.z) * this.m_axis.x,
                h = this.m_impulse.x * this.m_perp.y + (this.m_motorImpulse + this.m_impulse.z) * this.m_axis.y, g = this.m_impulse.x * this.m_s1 + this.m_impulse.y + (this.m_motorImpulse + this.m_impulse.z) * this.m_a1, f = this.m_impulse.x * this.m_s2 + this.m_impulse.y + (this.m_motorImpulse + this.m_impulse.z) * this.m_a2, c.m_linearVelocity.x -= this.m_invMassA * a, c.m_linearVelocity.y -= this.m_invMassA * h, c.m_angularVelocity -= this.m_invIA * g, d.m_linearVelocity.x += this.m_invMassB * a, d.m_linearVelocity.y += this.m_invMassB * h, d.m_angularVelocity += this.m_invIB * f) : (this.m_impulse.SetZero(), this.m_motorImpulse = 0)
            };
            J.prototype.SolveVelocityConstraints = function (a) {
                var b = this.m_bodyA,
                    c = this.m_bodyB,
                    d = b.m_linearVelocity,
                    g = b.m_angularVelocity,
                    m = c.m_linearVelocity,
                    i = c.m_angularVelocity,
                    e = 0,
                    k = 0,
                    o = 0,
                    r = 0;
                if (this.m_enableMotor && this.m_limitState != w.e_equalLimits) r = this.m_motorMass * (this.m_motorSpeed - (this.m_axis.x * (m.x - d.x) + this.m_axis.y * (m.y - d.y) + this.m_a2 * i - this.m_a1 * g)), e = this.m_motorImpulse, a = a.dt * this.m_maxMotorForce, this.m_motorImpulse = p.Clamp(this.m_motorImpulse + r, -a, a), r = this.m_motorImpulse - e, e = r * this.m_axis.x, k = r * this.m_axis.y, o = r * this.m_a1, r *= this.m_a2, d.x -= this.m_invMassA * e, d.y -= this.m_invMassA * k, g -= this.m_invIA * o, m.x += this.m_invMassB * e, m.y += this.m_invMassB * k, i += this.m_invIB * r;
                o = this.m_perp.x * (m.x - d.x) + this.m_perp.y * (m.y - d.y) + this.m_s2 * i - this.m_s1 * g;
                k = i - g;
                if (this.m_enableLimit && this.m_limitState != w.e_inactiveLimit) {
                    a = this.m_axis.x * (m.x - d.x) + this.m_axis.y * (m.y - d.y) + this.m_a2 * i - this.m_a1 * g;
                    e = this.m_impulse.Copy();
                    a = this.m_K.Solve33(new l, -o, -k, -a);
                    this.m_impulse.Add(a);
                    if (this.m_limitState == w.e_atLowerLimit) this.m_impulse.z = p.Max(this.m_impulse.z, 0);
                    else if (this.m_limitState == w.e_atUpperLimit) this.m_impulse.z = p.Min(this.m_impulse.z, 0);
                    o = -o - (this.m_impulse.z - e.z) * this.m_K.col3.x;
                    k = -k - (this.m_impulse.z - e.z) * this.m_K.col3.y;
                    k = this.m_K.Solve22(new f, o, k);
                    k.x += e.x;
                    k.y += e.y;
                    this.m_impulse.x = k.x;
                    this.m_impulse.y = k.y;
                    a.x = this.m_impulse.x - e.x;
                    a.y = this.m_impulse.y - e.y;
                    a.z = this.m_impulse.z - e.z;
                    e = a.x * this.m_perp.x + a.z * this.m_axis.x;
                    k = a.x * this.m_perp.y + a.z * this.m_axis.y;
                    o = a.x * this.m_s1 + a.y + a.z * this.m_a1;
                    r = a.x * this.m_s2 + a.y + a.z * this.m_a2
                } else a = this.m_K.Solve22(new f, -o, -k), this.m_impulse.x += a.x, this.m_impulse.y += a.y, e = a.x * this.m_perp.x, k = a.x * this.m_perp.y, o = a.x * this.m_s1 + a.y, r = a.x * this.m_s2 + a.y;
                d.x -= this.m_invMassA * e;
                d.y -= this.m_invMassA * k;
                g -= this.m_invIA * o;
                m.x += this.m_invMassB * e;
                m.y += this.m_invMassB * k;
                i += this.m_invIB * r;
                b.m_linearVelocity.SetV(d);
                b.m_angularVelocity = g;
                c.m_linearVelocity.SetV(m);
                c.m_angularVelocity = i
            };
            J.prototype.SolvePositionConstraints = function () {
                var a = this.m_bodyA,
                    d = this.m_bodyB,
                    g = a.m_sweep.c,
                    h = a.m_sweep.a,
                    m = d.m_sweep.c,
                    i = d.m_sweep.a,
                    o, e = 0,
                    k = 0,
                    r = 0,
                    s = e = o = 0,
                    t = 0,
                    k = !1,
                    z = 0,
                    A = c.FromAngle(h),
                    B = c.FromAngle(i);
                o = A;
                var t = this.m_localAnchor1.x - this.m_localCenterA.x,
                    w = this.m_localAnchor1.y - this.m_localCenterA.y,
                    e = o.col1.x * t + o.col2.x * w,
                    w = o.col1.y * t + o.col2.y * w,
                    t = e;
                o = B;
                B = this.m_localAnchor2.x - this.m_localCenterB.x;
                r = this.m_localAnchor2.y - this.m_localCenterB.y;
                e = o.col1.x * B + o.col2.x * r;
                r = o.col1.y * B + o.col2.y * r;
                B = e;
                o = m.x + B - g.x - t;
                e = m.y + r - g.y - w;
                if (this.m_enableLimit) {
                    this.m_axis = p.MulMV(A, this.m_localXAxis1);
                    this.m_a1 = (o + t) * this.m_axis.y - (e + w) * this.m_axis.x;
                    this.m_a2 = B * this.m_axis.y - r * this.m_axis.x;
                    var y = this.m_axis.x * o + this.m_axis.y * e;
                    p.Abs(this.m_upperTranslation - this.m_lowerTranslation) < 2 * b.b2_linearSlop ? (z = p.Clamp(y, -b.b2_maxLinearCorrection, b.b2_maxLinearCorrection), s = p.Abs(y), k = !0) : y <= this.m_lowerTranslation ? (z = p.Clamp(y - this.m_lowerTranslation + b.b2_linearSlop, -b.b2_maxLinearCorrection, 0), s = this.m_lowerTranslation - y, k = !0) : y >= this.m_upperTranslation && (z = p.Clamp(y - this.m_upperTranslation + b.b2_linearSlop, 0, b.b2_maxLinearCorrection), s = y - this.m_upperTranslation, k = !0)
                }
                this.m_perp = p.MulMV(A, this.m_localYAxis1);
                this.m_s1 = (o + t) * this.m_perp.y - (e + w) * this.m_perp.x;
                this.m_s2 = B * this.m_perp.y - r * this.m_perp.x;
                A = new l;
                w = this.m_perp.x * o + this.m_perp.y * e;
                B = i - h - this.m_refAngle;
                s = p.Max(s, p.Abs(w));
                t = p.Abs(B);
                k ? (k = this.m_invMassA, r = this.m_invMassB, o = this.m_invIA, e = this.m_invIB, this.m_K.col1.x = k + r + o * this.m_s1 * this.m_s1 + e * this.m_s2 * this.m_s2, this.m_K.col1.y = o * this.m_s1 + e * this.m_s2, this.m_K.col1.z = o * this.m_s1 * this.m_a1 + e * this.m_s2 * this.m_a2, this.m_K.col2.x = this.m_K.col1.y, this.m_K.col2.y = o + e, this.m_K.col2.z = o * this.m_a1 + e * this.m_a2, this.m_K.col3.x = this.m_K.col1.z, this.m_K.col3.y = this.m_K.col2.z, this.m_K.col3.z = k + r + o * this.m_a1 * this.m_a1 + e * this.m_a2 * this.m_a2, this.m_K.Solve33(A, -w, -B, -z)) : (k = this.m_invMassA, r = this.m_invMassB, o = this.m_invIA, e = this.m_invIB, z = o * this.m_s1 + e * this.m_s2, y = o + e, this.m_K.col1.Set(k + r + o * this.m_s1 * this.m_s1 + e * this.m_s2 * this.m_s2, z, 0), this.m_K.col2.Set(z, y, 0), z = this.m_K.Solve22(new f, -w, -B), A.x = z.x, A.y = z.y, A.z = 0);
                z = A.x * this.m_perp.x + A.z * this.m_axis.x;
                k = A.x * this.m_perp.y + A.z * this.m_axis.y;
                w = A.x * this.m_s1 + A.y + A.z * this.m_a1;
                A = A.x * this.m_s2 + A.y + A.z * this.m_a2;
                g.x -= this.m_invMassA * z;
                g.y -= this.m_invMassA * k;
                h -= this.m_invIA * w;
                m.x += this.m_invMassB * z;
                m.y += this.m_invMassB * k;
                i += this.m_invIB * A;
                a.m_sweep.a = h;
                d.m_sweep.a = i;
                a.SynchronizeTransform();
                d.SynchronizeTransform();
                return s <= b.b2_linearSlop && t <= b.b2_angularSlop
            };
            a.inherit(K, a.Dynamics.Joints.b2JointDef);
            K.prototype.__super = a.Dynamics.Joints.b2JointDef.prototype;
            K.b2PrismaticJointDef = function () {
                a.Dynamics.Joints.b2JointDef.b2JointDef.apply(this, arguments);
                this.localAnchorA = new f;
                this.localAnchorB = new f;
                this.localAxisA = new f
            };
            K.prototype.b2PrismaticJointDef = function () {
                this.__super.b2JointDef.call(this);
                this.type = w.e_prismaticJoint;
                this.localAxisA.Set(1, 0);
                this.referenceAngle = 0;
                this.enableLimit = !1;
                this.upperTranslation = this.lowerTranslation = 0;
                this.enableMotor = !1;
                this.motorSpeed = this.maxMotorForce = 0
            };
            K.prototype.Initialize = function (a, b, c, d) {
                this.bodyA = a;
                this.bodyB = b;
                this.localAnchorA = this.bodyA.GetLocalPoint(c);
                this.localAnchorB = this.bodyB.GetLocalPoint(c);
                this.localAxisA = this.bodyA.GetLocalVector(d);
                this.referenceAngle = this.bodyB.GetAngle() - this.bodyA.GetAngle()
            };
            a.inherit(C, a.Dynamics.Joints.b2Joint);
            C.prototype.__super = a.Dynamics.Joints.b2Joint.prototype;
            C.b2PulleyJoint = function () {
                a.Dynamics.Joints.b2Joint.b2Joint.apply(this, arguments);
                this.m_groundAnchor1 = new f;
                this.m_groundAnchor2 = new f;
                this.m_localAnchor1 = new f;
                this.m_localAnchor2 = new f;
                this.m_u1 = new f;
                this.m_u2 = new f
            };
            C.prototype.GetAnchorA = function () {
                return this.m_bodyA.GetWorldPoint(this.m_localAnchor1)
            };
            C.prototype.GetAnchorB = function () {
                return this.m_bodyB.GetWorldPoint(this.m_localAnchor2)
            };
            C.prototype.GetReactionForce = function (a) {
                a === void 0 && (a = 0);
                return new f(a * this.m_impulse * this.m_u2.x, a * this.m_impulse * this.m_u2.y)
            };
            C.prototype.GetReactionTorque = function () {
                return 0
            };
            C.prototype.GetGroundAnchorA = function () {
                var a = this.m_ground.m_xf.position.Copy();
                a.Add(this.m_groundAnchor1);
                return a
            };
            C.prototype.GetGroundAnchorB = function () {
                var a = this.m_ground.m_xf.position.Copy();
                a.Add(this.m_groundAnchor2);
                return a
            };
            C.prototype.GetLength1 = function () {
                var a = this.m_bodyA.GetWorldPoint(this.m_localAnchor1),
                    b = a.x - (this.m_ground.m_xf.position.x + this.m_groundAnchor1.x),
                    a = a.y - (this.m_ground.m_xf.position.y + this.m_groundAnchor1.y);
                return Math.sqrt(b * b + a * a)
            };
            C.prototype.GetLength2 = function () {
                var a = this.m_bodyB.GetWorldPoint(this.m_localAnchor2),
                    b = a.x - (this.m_ground.m_xf.position.x + this.m_groundAnchor2.x),
                    a = a.y - (this.m_ground.m_xf.position.y + this.m_groundAnchor2.y);
                return Math.sqrt(b * b + a * a)
            };
            C.prototype.GetRatio = function () {
                return this.m_ratio
            };
            C.prototype.b2PulleyJoint = function (a) {
                this.__super.b2Joint.call(this, a);
                this.m_ground = this.m_bodyA.m_world.m_groundBody;
                this.m_groundAnchor1.x = a.groundAnchorA.x - this.m_ground.m_xf.position.x;
                this.m_groundAnchor1.y = a.groundAnchorA.y - this.m_ground.m_xf.position.y;
                this.m_groundAnchor2.x = a.groundAnchorB.x - this.m_ground.m_xf.position.x;
                this.m_groundAnchor2.y = a.groundAnchorB.y - this.m_ground.m_xf.position.y;
                this.m_localAnchor1.SetV(a.localAnchorA);
                this.m_localAnchor2.SetV(a.localAnchorB);
                this.m_ratio = a.ratio;
                this.m_constant = a.lengthA + this.m_ratio * a.lengthB;
                this.m_maxLength1 = p.Min(a.maxLengthA, this.m_constant - this.m_ratio * C.b2_minPulleyLength);
                this.m_maxLength2 = p.Min(a.maxLengthB, (this.m_constant - C.b2_minPulleyLength) / this.m_ratio);
                this.m_limitImpulse2 = this.m_limitImpulse1 = this.m_impulse = 0
            };
            C.prototype.InitVelocityConstraints = function (a) {
                var c = this.m_bodyA,
                    d = this.m_bodyB,
                    h;
                h = c.m_xf.R;
                var g = this.m_localAnchor1.x - c.m_sweep.localCenter.x,
                    f = this.m_localAnchor1.y - c.m_sweep.localCenter.y,
                    m = h.col1.x * g + h.col2.x * f,
                    f = h.col1.y * g + h.col2.y * f,
                    g = m;
                h = d.m_xf.R;
                var e = this.m_localAnchor2.x - d.m_sweep.localCenter.x,
                    k = this.m_localAnchor2.y - d.m_sweep.localCenter.y,
                    m = h.col1.x * e + h.col2.x * k,
                    k = h.col1.y * e + h.col2.y * k,
                    e = m;
                h = d.m_sweep.c.x + e;
                var m = d.m_sweep.c.y + k,
                    l = this.m_ground.m_xf.position.x + this.m_groundAnchor2.x,
                    p = this.m_ground.m_xf.position.y + this.m_groundAnchor2.y;
                this.m_u1.Set(c.m_sweep.c.x + g - (this.m_ground.m_xf.position.x + this.m_groundAnchor1.x),
                c.m_sweep.c.y + f - (this.m_ground.m_xf.position.y + this.m_groundAnchor1.y));
                this.m_u2.Set(h - l, m - p);
                h = this.m_u1.Length();
                m = this.m_u2.Length();
                h > b.b2_linearSlop ? this.m_u1.Multiply(1 / h) : this.m_u1.SetZero();
                m > b.b2_linearSlop ? this.m_u2.Multiply(1 / m) : this.m_u2.SetZero();
                this.m_constant - h - this.m_ratio * m > 0 ? (this.m_state = w.e_inactiveLimit, this.m_impulse = 0) : this.m_state = w.e_atUpperLimit;
                h < this.m_maxLength1 ? (this.m_limitState1 = w.e_inactiveLimit, this.m_limitImpulse1 = 0) : this.m_limitState1 = w.e_atUpperLimit;
                m < this.m_maxLength2 ? (this.m_limitState2 = w.e_inactiveLimit, this.m_limitImpulse2 = 0) : this.m_limitState2 = w.e_atUpperLimit;
                h = g * this.m_u1.y - f * this.m_u1.x;
                m = e * this.m_u2.y - k * this.m_u2.x;
                this.m_limitMass1 = c.m_invMass + c.m_invI * h * h;
                this.m_limitMass2 = d.m_invMass + d.m_invI * m * m;
                this.m_pulleyMass = this.m_limitMass1 + this.m_ratio * this.m_ratio * this.m_limitMass2;
                this.m_limitMass1 = 1 / this.m_limitMass1;
                this.m_limitMass2 = 1 / this.m_limitMass2;
                this.m_pulleyMass = 1 / this.m_pulleyMass;
                a.warmStarting ? (this.m_impulse *= a.dtRatio, this.m_limitImpulse1 *= a.dtRatio, this.m_limitImpulse2 *= a.dtRatio, a = (-this.m_impulse - this.m_limitImpulse1) * this.m_u1.x, h = (-this.m_impulse - this.m_limitImpulse1) * this.m_u1.y, m = (-this.m_ratio * this.m_impulse - this.m_limitImpulse2) * this.m_u2.x, l = (-this.m_ratio * this.m_impulse - this.m_limitImpulse2) * this.m_u2.y, c.m_linearVelocity.x += c.m_invMass * a, c.m_linearVelocity.y += c.m_invMass * h, c.m_angularVelocity += c.m_invI * (g * h - f * a), d.m_linearVelocity.x += d.m_invMass * m, d.m_linearVelocity.y += d.m_invMass * l, d.m_angularVelocity += d.m_invI * (e * l - k * m)) : this.m_limitImpulse2 = this.m_limitImpulse1 = this.m_impulse = 0
            };
            C.prototype.SolveVelocityConstraints = function () {
                var a = this.m_bodyA,
                    b = this.m_bodyB,
                    c;
                c = a.m_xf.R;
                var d = this.m_localAnchor1.x - a.m_sweep.localCenter.x,
                    g = this.m_localAnchor1.y - a.m_sweep.localCenter.y,
                    f = c.col1.x * d + c.col2.x * g,
                    g = c.col1.y * d + c.col2.y * g,
                    d = f;
                c = b.m_xf.R;
                var m = this.m_localAnchor2.x - b.m_sweep.localCenter.x,
                    e = this.m_localAnchor2.y - b.m_sweep.localCenter.y,
                    f = c.col1.x * m + c.col2.x * e,
                    e = c.col1.y * m + c.col2.y * e,
                    m = f,
                    k = f = c = 0,
                    l = 0;
                c = l = c = l = k = f = c = 0;
                if (this.m_state == w.e_atUpperLimit) c = a.m_linearVelocity.x + -a.m_angularVelocity * g, f = a.m_linearVelocity.y + a.m_angularVelocity * d, k = b.m_linearVelocity.x + -b.m_angularVelocity * e, l = b.m_linearVelocity.y + b.m_angularVelocity * m, c = -(this.m_u1.x * c + this.m_u1.y * f) - this.m_ratio * (this.m_u2.x * k + this.m_u2.y * l), l = this.m_pulleyMass * -c, c = this.m_impulse, this.m_impulse = p.Max(0, this.m_impulse + l), l = this.m_impulse - c, c = -l * this.m_u1.x, f = -l * this.m_u1.y, k = -this.m_ratio * l * this.m_u2.x, l = -this.m_ratio * l * this.m_u2.y, a.m_linearVelocity.x += a.m_invMass * c, a.m_linearVelocity.y += a.m_invMass * f, a.m_angularVelocity += a.m_invI * (d * f - g * c), b.m_linearVelocity.x += b.m_invMass * k, b.m_linearVelocity.y += b.m_invMass * l, b.m_angularVelocity += b.m_invI * (m * l - e * k);
                if (this.m_limitState1 == w.e_atUpperLimit) c = a.m_linearVelocity.x + -a.m_angularVelocity * g, f = a.m_linearVelocity.y + a.m_angularVelocity * d, c = -(this.m_u1.x * c + this.m_u1.y * f), l = -this.m_limitMass1 * c, c = this.m_limitImpulse1, this.m_limitImpulse1 = p.Max(0, this.m_limitImpulse1 + l), l = this.m_limitImpulse1 - c, c = -l * this.m_u1.x,
                f = -l * this.m_u1.y, a.m_linearVelocity.x += a.m_invMass * c, a.m_linearVelocity.y += a.m_invMass * f, a.m_angularVelocity += a.m_invI * (d * f - g * c);
                if (this.m_limitState2 == w.e_atUpperLimit) k = b.m_linearVelocity.x + -b.m_angularVelocity * e, l = b.m_linearVelocity.y + b.m_angularVelocity * m, c = -(this.m_u2.x * k + this.m_u2.y * l), l = -this.m_limitMass2 * c, c = this.m_limitImpulse2, this.m_limitImpulse2 = p.Max(0, this.m_limitImpulse2 + l), l = this.m_limitImpulse2 - c, k = -l * this.m_u2.x, l = -l * this.m_u2.y, b.m_linearVelocity.x += b.m_invMass * k, b.m_linearVelocity.y += b.m_invMass * l, b.m_angularVelocity += b.m_invI * (m * l - e * k)
            };
            C.prototype.SolvePositionConstraints = function () {
                var a = this.m_bodyA,
                    c = this.m_bodyB,
                    d, h = this.m_ground.m_xf.position.x + this.m_groundAnchor1.x,
                    g = this.m_ground.m_xf.position.y + this.m_groundAnchor1.y,
                    f = this.m_ground.m_xf.position.x + this.m_groundAnchor2.x,
                    m = this.m_ground.m_xf.position.y + this.m_groundAnchor2.y,
                    e = 0,
                    k = 0,
                    l = 0,
                    o = 0,
                    i = d = 0,
                    r = 0,
                    s = 0,
                    t = i = s = d = i = d = 0;
                if (this.m_state == w.e_atUpperLimit) d = a.m_xf.R, e = this.m_localAnchor1.x - a.m_sweep.localCenter.x, k = this.m_localAnchor1.y - a.m_sweep.localCenter.y, i = d.col1.x * e + d.col2.x * k, k = d.col1.y * e + d.col2.y * k, e = i, d = c.m_xf.R, l = this.m_localAnchor2.x - c.m_sweep.localCenter.x, o = this.m_localAnchor2.y - c.m_sweep.localCenter.y, i = d.col1.x * l + d.col2.x * o, o = d.col1.y * l + d.col2.y * o, l = i, d = a.m_sweep.c.x + e, i = a.m_sweep.c.y + k, r = c.m_sweep.c.x + l, s = c.m_sweep.c.y + o, this.m_u1.Set(d - h, i - g), this.m_u2.Set(r - f, s - m), d = this.m_u1.Length(), i = this.m_u2.Length(), d > b.b2_linearSlop ? this.m_u1.Multiply(1 / d) : this.m_u1.SetZero(), i > b.b2_linearSlop ? this.m_u2.Multiply(1 / i) : this.m_u2.SetZero(),
                d = this.m_constant - d - this.m_ratio * i, t = p.Max(t, -d), d = p.Clamp(d + b.b2_linearSlop, -b.b2_maxLinearCorrection, 0), s = -this.m_pulleyMass * d, d = -s * this.m_u1.x, i = -s * this.m_u1.y, r = -this.m_ratio * s * this.m_u2.x, s = -this.m_ratio * s * this.m_u2.y, a.m_sweep.c.x += a.m_invMass * d, a.m_sweep.c.y += a.m_invMass * i, a.m_sweep.a += a.m_invI * (e * i - k * d), c.m_sweep.c.x += c.m_invMass * r, c.m_sweep.c.y += c.m_invMass * s, c.m_sweep.a += c.m_invI * (l * s - o * r), a.SynchronizeTransform(), c.SynchronizeTransform();
                if (this.m_limitState1 == w.e_atUpperLimit) d = a.m_xf.R,
                e = this.m_localAnchor1.x - a.m_sweep.localCenter.x, k = this.m_localAnchor1.y - a.m_sweep.localCenter.y, i = d.col1.x * e + d.col2.x * k, k = d.col1.y * e + d.col2.y * k, e = i, d = a.m_sweep.c.x + e, i = a.m_sweep.c.y + k, this.m_u1.Set(d - h, i - g), d = this.m_u1.Length(), d > b.b2_linearSlop ? (this.m_u1.x *= 1 / d, this.m_u1.y *= 1 / d) : this.m_u1.SetZero(), d = this.m_maxLength1 - d, t = p.Max(t, -d), d = p.Clamp(d + b.b2_linearSlop, -b.b2_maxLinearCorrection, 0), s = -this.m_limitMass1 * d, d = -s * this.m_u1.x, i = -s * this.m_u1.y, a.m_sweep.c.x += a.m_invMass * d, a.m_sweep.c.y += a.m_invMass * i, a.m_sweep.a += a.m_invI * (e * i - k * d), a.SynchronizeTransform();
                if (this.m_limitState2 == w.e_atUpperLimit) d = c.m_xf.R, l = this.m_localAnchor2.x - c.m_sweep.localCenter.x, o = this.m_localAnchor2.y - c.m_sweep.localCenter.y, i = d.col1.x * l + d.col2.x * o, o = d.col1.y * l + d.col2.y * o, l = i, r = c.m_sweep.c.x + l, s = c.m_sweep.c.y + o, this.m_u2.Set(r - f, s - m), i = this.m_u2.Length(), i > b.b2_linearSlop ? (this.m_u2.x *= 1 / i, this.m_u2.y *= 1 / i) : this.m_u2.SetZero(), d = this.m_maxLength2 - i, t = p.Max(t, -d), d = p.Clamp(d + b.b2_linearSlop, -b.b2_maxLinearCorrection,
                0), s = -this.m_limitMass2 * d, r = -s * this.m_u2.x, s = -s * this.m_u2.y, c.m_sweep.c.x += c.m_invMass * r, c.m_sweep.c.y += c.m_invMass * s, c.m_sweep.a += c.m_invI * (l * s - o * r), c.SynchronizeTransform();
                return t < b.b2_linearSlop
            };
            a.postDefs.push(function () {
                a.Dynamics.Joints.b2PulleyJoint.b2_minPulleyLength = 2
            });
            a.inherit(O, a.Dynamics.Joints.b2JointDef);
            O.prototype.__super = a.Dynamics.Joints.b2JointDef.prototype;
            O.b2PulleyJointDef = function () {
                a.Dynamics.Joints.b2JointDef.b2JointDef.apply(this, arguments);
                this.groundAnchorA = new f;
                this.groundAnchorB = new f;
                this.localAnchorA = new f;
                this.localAnchorB = new f
            };
            O.prototype.b2PulleyJointDef = function () {
                this.__super.b2JointDef.call(this);
                this.type = w.e_pulleyJoint;
                this.groundAnchorA.Set(-1, 1);
                this.groundAnchorB.Set(1, 1);
                this.localAnchorA.Set(-1, 0);
                this.localAnchorB.Set(1, 0);
                this.maxLengthB = this.lengthB = this.maxLengthA = this.lengthA = 0;
                this.ratio = 1;
                this.collideConnected = !0
            };
            O.prototype.Initialize = function (a, b, c, d, g, f, m) {
                m === void 0 && (m = 0);
                this.bodyA = a;
                this.bodyB = b;
                this.groundAnchorA.SetV(c);
                this.groundAnchorB.SetV(d);
                this.localAnchorA = this.bodyA.GetLocalPoint(g);
                this.localAnchorB = this.bodyB.GetLocalPoint(f);
                a = g.x - c.x;
                c = g.y - c.y;
                this.lengthA = Math.sqrt(a * a + c * c);
                c = f.x - d.x;
                d = f.y - d.y;
                this.lengthB = Math.sqrt(c * c + d * d);
                this.ratio = m;
                m = this.lengthA + this.ratio * this.lengthB;
                this.maxLengthA = m - this.ratio * C.b2_minPulleyLength;
                this.maxLengthB = (m - C.b2_minPulleyLength) / this.ratio
            };
            a.inherit(E, a.Dynamics.Joints.b2Joint);
            E.prototype.__super = a.Dynamics.Joints.b2Joint.prototype;
            E.b2RevoluteJoint = function () {
                a.Dynamics.Joints.b2Joint.b2Joint.apply(this,
                arguments);
                this.K = new c;
                this.K1 = new c;
                this.K2 = new c;
                this.K3 = new c;
                this.impulse3 = new l;
                this.impulse2 = new f;
                this.reduced = new f;
                this.m_localAnchor1 = new f;
                this.m_localAnchor2 = new f;
                this.m_impulse = new l;
                this.m_mass = new g
            };
            E.prototype.GetAnchorA = function () {
                return this.m_bodyA.GetWorldPoint(this.m_localAnchor1)
            };
            E.prototype.GetAnchorB = function () {
                return this.m_bodyB.GetWorldPoint(this.m_localAnchor2)
            };
            E.prototype.GetReactionForce = function (a) {
                a === void 0 && (a = 0);
                return new f(a * this.m_impulse.x, a * this.m_impulse.y)
            };
            E.prototype.GetReactionTorque = function (a) {
                a === void 0 && (a = 0);
                return a * this.m_impulse.z
            };
            E.prototype.GetJointAngle = function () {
                return this.m_bodyB.m_sweep.a - this.m_bodyA.m_sweep.a - this.m_referenceAngle
            };
            E.prototype.GetJointSpeed = function () {
                return this.m_bodyB.m_angularVelocity - this.m_bodyA.m_angularVelocity
            };
            E.prototype.IsLimitEnabled = function () {
                return this.m_enableLimit
            };
            E.prototype.EnableLimit = function (a) {
                this.m_enableLimit = a
            };
            E.prototype.GetLowerLimit = function () {
                return this.m_lowerAngle
            };
            E.prototype.GetUpperLimit = function () {
                return this.m_upperAngle
            };
            E.prototype.SetLimits = function (a, b) {
                a === void 0 && (a = 0);
                b === void 0 && (b = 0);
                this.m_lowerAngle = a;
                this.m_upperAngle = b
            };
            E.prototype.IsMotorEnabled = function () {
                this.m_bodyA.SetAwake(!0);
                this.m_bodyB.SetAwake(!0);
                return this.m_enableMotor
            };
            E.prototype.EnableMotor = function (a) {
                this.m_enableMotor = a
            };
            E.prototype.SetMotorSpeed = function (a) {
                a === void 0 && (a = 0);
                this.m_bodyA.SetAwake(!0);
                this.m_bodyB.SetAwake(!0);
                this.m_motorSpeed = a
            };
            E.prototype.GetMotorSpeed = function () {
                return this.m_motorSpeed
            };
            E.prototype.SetMaxMotorTorque = function (a) {
                a === void 0 && (a = 0);
                this.m_maxMotorTorque = a
            };
            E.prototype.GetMotorTorque = function () {
                return this.m_maxMotorTorque
            };
            E.prototype.b2RevoluteJoint = function (a) {
                this.__super.b2Joint.call(this, a);
                this.m_localAnchor1.SetV(a.localAnchorA);
                this.m_localAnchor2.SetV(a.localAnchorB);
                this.m_referenceAngle = a.referenceAngle;
                this.m_impulse.SetZero();
                this.m_motorImpulse = 0;
                this.m_lowerAngle = a.lowerAngle;
                this.m_upperAngle = a.upperAngle;
                this.m_maxMotorTorque = a.maxMotorTorque;
                this.m_motorSpeed = a.motorSpeed;
                this.m_enableLimit = a.enableLimit;
                this.m_enableMotor = a.enableMotor;
                this.m_limitState = w.e_inactiveLimit
            };
            E.prototype.InitVelocityConstraints = function (a) {
                var c = this.m_bodyA,
                    d = this.m_bodyB,
                    h, g = 0;
                h = c.m_xf.R;
                var f = this.m_localAnchor1.x - c.m_sweep.localCenter.x,
                    m = this.m_localAnchor1.y - c.m_sweep.localCenter.y,
                    g = h.col1.x * f + h.col2.x * m,
                    m = h.col1.y * f + h.col2.y * m,
                    f = g;
                h = d.m_xf.R;
                var e = this.m_localAnchor2.x - d.m_sweep.localCenter.x,
                    k = this.m_localAnchor2.y - d.m_sweep.localCenter.y,
                    g = h.col1.x * e + h.col2.x * k,
                    k = h.col1.y * e + h.col2.y * k,
                    e = g;
                h = c.m_invMass;
                var g = d.m_invMass,
                    l = c.m_invI,
                    i = d.m_invI;
                this.m_mass.col1.x = h + g + m * m * l + k * k * i;
                this.m_mass.col2.x = -m * f * l - k * e * i;
                this.m_mass.col3.x = -m * l - k * i;
                this.m_mass.col1.y = this.m_mass.col2.x;
                this.m_mass.col2.y = h + g + f * f * l + e * e * i;
                this.m_mass.col3.y = f * l + e * i;
                this.m_mass.col1.z = this.m_mass.col3.x;
                this.m_mass.col2.z = this.m_mass.col3.y;
                this.m_mass.col3.z = l + i;
                this.m_motorMass = 1 / (l + i);
                if (this.m_enableMotor == !1) this.m_motorImpulse = 0;
                if (this.m_enableLimit) {
                    var o = d.m_sweep.a - c.m_sweep.a - this.m_referenceAngle;
                    if (p.Abs(this.m_upperAngle - this.m_lowerAngle) < 2 * b.b2_angularSlop) this.m_limitState = w.e_equalLimits;
                    else if (o <= this.m_lowerAngle) {
                        if (this.m_limitState != w.e_atLowerLimit) this.m_impulse.z = 0;
                        this.m_limitState = w.e_atLowerLimit
                    } else if (o >= this.m_upperAngle) {
                        if (this.m_limitState != w.e_atUpperLimit) this.m_impulse.z = 0;
                        this.m_limitState = w.e_atUpperLimit
                    } else this.m_limitState = w.e_inactiveLimit, this.m_impulse.z = 0
                } else this.m_limitState = w.e_inactiveLimit;
                a.warmStarting ? (this.m_impulse.x *= a.dtRatio, this.m_impulse.y *= a.dtRatio, this.m_motorImpulse *= a.dtRatio, a = this.m_impulse.x, o = this.m_impulse.y, c.m_linearVelocity.x -= h * a, c.m_linearVelocity.y -= h * o, c.m_angularVelocity -= l * (f * o - m * a + this.m_motorImpulse + this.m_impulse.z), d.m_linearVelocity.x += g * a, d.m_linearVelocity.y += g * o, d.m_angularVelocity += i * (e * o - k * a + this.m_motorImpulse + this.m_impulse.z)) : (this.m_impulse.SetZero(), this.m_motorImpulse = 0)
            };
            E.prototype.SolveVelocityConstraints = function (a) {
                var b = this.m_bodyA,
                    c = this.m_bodyB,
                    d = 0,
                    g = d = 0,
                    f = 0,
                    m = 0,
                    e = 0,
                    k = b.m_linearVelocity,
                    l = b.m_angularVelocity,
                    i = c.m_linearVelocity,
                    o = c.m_angularVelocity,
                    r = b.m_invMass,
                    s = c.m_invMass,
                    t = b.m_invI,
                    z = c.m_invI;
                if (this.m_enableMotor && this.m_limitState != w.e_equalLimits) g = this.m_motorMass * -(o - l - this.m_motorSpeed), f = this.m_motorImpulse, m = a.dt * this.m_maxMotorTorque, this.m_motorImpulse = p.Clamp(this.m_motorImpulse + g, -m, m), g = this.m_motorImpulse - f, l -= t * g, o += z * g;
                if (this.m_enableLimit && this.m_limitState != w.e_inactiveLimit) {
                    var a = b.m_xf.R,
                        g = this.m_localAnchor1.x - b.m_sweep.localCenter.x,
                        f = this.m_localAnchor1.y - b.m_sweep.localCenter.y,
                        d = a.col1.x * g + a.col2.x * f,
                        f = a.col1.y * g + a.col2.y * f,
                        g = d,
                        a = c.m_xf.R,
                        m = this.m_localAnchor2.x - c.m_sweep.localCenter.x,
                        e = this.m_localAnchor2.y - c.m_sweep.localCenter.y,
                        d = a.col1.x * m + a.col2.x * e,
                        e = a.col1.y * m + a.col2.y * e,
                        m = d,
                        a = i.x + -o * e - k.x - -l * f,
                        A = i.y + o * m - k.y - l * g;
                    this.m_mass.Solve33(this.impulse3, -a, -A, -(o - l));
                    if (this.m_limitState == w.e_equalLimits) this.m_impulse.Add(this.impulse3);
                    else if (this.m_limitState == w.e_atLowerLimit) {
                        if (d = this.m_impulse.z + this.impulse3.z,
                        d < 0) this.m_mass.Solve22(this.reduced, -a, -A), this.impulse3.x = this.reduced.x, this.impulse3.y = this.reduced.y, this.impulse3.z = -this.m_impulse.z, this.m_impulse.x += this.reduced.x, this.m_impulse.y += this.reduced.y, this.m_impulse.z = 0
                    } else if (this.m_limitState == w.e_atUpperLimit && (d = this.m_impulse.z + this.impulse3.z, d > 0)) this.m_mass.Solve22(this.reduced, -a, -A), this.impulse3.x = this.reduced.x, this.impulse3.y = this.reduced.y, this.impulse3.z = -this.m_impulse.z, this.m_impulse.x += this.reduced.x, this.m_impulse.y += this.reduced.y,
                    this.m_impulse.z = 0;
                    k.x -= r * this.impulse3.x;
                    k.y -= r * this.impulse3.y;
                    l -= t * (g * this.impulse3.y - f * this.impulse3.x + this.impulse3.z);
                    i.x += s * this.impulse3.x;
                    i.y += s * this.impulse3.y;
                    o += z * (m * this.impulse3.y - e * this.impulse3.x + this.impulse3.z)
                } else a = b.m_xf.R, g = this.m_localAnchor1.x - b.m_sweep.localCenter.x, f = this.m_localAnchor1.y - b.m_sweep.localCenter.y, d = a.col1.x * g + a.col2.x * f, f = a.col1.y * g + a.col2.y * f, g = d, a = c.m_xf.R, m = this.m_localAnchor2.x - c.m_sweep.localCenter.x, e = this.m_localAnchor2.y - c.m_sweep.localCenter.y, d = a.col1.x * m + a.col2.x * e, e = a.col1.y * m + a.col2.y * e, m = d, this.m_mass.Solve22(this.impulse2, -(i.x + -o * e - k.x - -l * f), -(i.y + o * m - k.y - l * g)), this.m_impulse.x += this.impulse2.x, this.m_impulse.y += this.impulse2.y, k.x -= r * this.impulse2.x, k.y -= r * this.impulse2.y, l -= t * (g * this.impulse2.y - f * this.impulse2.x), i.x += s * this.impulse2.x, i.y += s * this.impulse2.y, o += z * (m * this.impulse2.y - e * this.impulse2.x);
                b.m_linearVelocity.SetV(k);
                b.m_angularVelocity = l;
                c.m_linearVelocity.SetV(i);
                c.m_angularVelocity = o
            };
            E.prototype.SolvePositionConstraints = function () {
                var a = 0,
                    c, d = this.m_bodyA,
                    h = this.m_bodyB,
                    g = 0,
                    f = c = 0,
                    m = 0,
                    e = 0;
                if (this.m_enableLimit && this.m_limitState != w.e_inactiveLimit) {
                    var a = h.m_sweep.a - d.m_sweep.a - this.m_referenceAngle,
                        k = 0;
                    this.m_limitState == w.e_equalLimits ? (a = p.Clamp(a - this.m_lowerAngle, -b.b2_maxAngularCorrection, b.b2_maxAngularCorrection), k = -this.m_motorMass * a, g = p.Abs(a)) : this.m_limitState == w.e_atLowerLimit ? (a -= this.m_lowerAngle, g = -a, a = p.Clamp(a + b.b2_angularSlop, -b.b2_maxAngularCorrection, 0), k = -this.m_motorMass * a) : this.m_limitState == w.e_atUpperLimit && (a -= this.m_upperAngle, g = a, a = p.Clamp(a - b.b2_angularSlop, 0, b.b2_maxAngularCorrection), k = -this.m_motorMass * a);
                    d.m_sweep.a -= d.m_invI * k;
                    h.m_sweep.a += h.m_invI * k;
                    d.SynchronizeTransform();
                    h.SynchronizeTransform()
                }
                c = d.m_xf.R;
                k = this.m_localAnchor1.x - d.m_sweep.localCenter.x;
                a = this.m_localAnchor1.y - d.m_sweep.localCenter.y;
                f = c.col1.x * k + c.col2.x * a;
                a = c.col1.y * k + c.col2.y * a;
                k = f;
                c = h.m_xf.R;
                var l = this.m_localAnchor2.x - h.m_sweep.localCenter.x,
                    i = this.m_localAnchor2.y - h.m_sweep.localCenter.y,
                    f = c.col1.x * l + c.col2.x * i,
                    i = c.col1.y * l + c.col2.y * i,
                    l = f,
                    m = h.m_sweep.c.x + l - d.m_sweep.c.x - k,
                    e = h.m_sweep.c.y + i - d.m_sweep.c.y - a,
                    o = m * m + e * e;
                c = Math.sqrt(o);
                var f = d.m_invMass,
                    r = h.m_invMass,
                    s = d.m_invI,
                    t = h.m_invI,
                    z = 10 * b.b2_linearSlop;
                o > z * z && (o = 1 / (f + r), m = o * -m, e = o * -e, d.m_sweep.c.x -= 0.5 * f * m, d.m_sweep.c.y -= 0.5 * f * e, h.m_sweep.c.x += 0.5 * r * m, h.m_sweep.c.y += 0.5 * r * e, m = h.m_sweep.c.x + l - d.m_sweep.c.x - k, e = h.m_sweep.c.y + i - d.m_sweep.c.y - a);
                this.K1.col1.x = f + r;
                this.K1.col2.x = 0;
                this.K1.col1.y = 0;
                this.K1.col2.y = f + r;
                this.K2.col1.x = s * a * a;
                this.K2.col2.x = -s * k * a;
                this.K2.col1.y = -s * k * a;
                this.K2.col2.y = s * k * k;
                this.K3.col1.x = t * i * i;
                this.K3.col2.x = -t * l * i;
                this.K3.col1.y = -t * l * i;
                this.K3.col2.y = t * l * l;
                this.K.SetM(this.K1);
                this.K.AddM(this.K2);
                this.K.AddM(this.K3);
                this.K.Solve(E.tImpulse, -m, -e);
                m = E.tImpulse.x;
                e = E.tImpulse.y;
                d.m_sweep.c.x -= d.m_invMass * m;
                d.m_sweep.c.y -= d.m_invMass * e;
                d.m_sweep.a -= d.m_invI * (k * e - a * m);
                h.m_sweep.c.x += h.m_invMass * m;
                h.m_sweep.c.y += h.m_invMass * e;
                h.m_sweep.a += h.m_invI * (l * e - i * m);
                d.SynchronizeTransform();
                h.SynchronizeTransform();
                return c <= b.b2_linearSlop && g <= b.b2_angularSlop
            };
            a.postDefs.push(function () {
                a.Dynamics.Joints.b2RevoluteJoint.tImpulse = new f
            });
            a.inherit(H, a.Dynamics.Joints.b2JointDef);
            H.prototype.__super = a.Dynamics.Joints.b2JointDef.prototype;
            H.b2RevoluteJointDef = function () {
                a.Dynamics.Joints.b2JointDef.b2JointDef.apply(this, arguments);
                this.localAnchorA = new f;
                this.localAnchorB = new f
            };
            H.prototype.b2RevoluteJointDef = function () {
                this.__super.b2JointDef.call(this);
                this.type = w.e_revoluteJoint;
                this.localAnchorA.Set(0, 0);
                this.localAnchorB.Set(0, 0);
                this.motorSpeed = this.maxMotorTorque = this.upperAngle = this.lowerAngle = this.referenceAngle = 0;
                this.enableMotor = this.enableLimit = !1
            };
            H.prototype.Initialize = function (a, b, c) {
                this.bodyA = a;
                this.bodyB = b;
                this.localAnchorA = this.bodyA.GetLocalPoint(c);
                this.localAnchorB = this.bodyB.GetLocalPoint(c);
                this.referenceAngle = this.bodyB.GetAngle() - this.bodyA.GetAngle()
            };
            a.inherit(P, a.Dynamics.Joints.b2Joint);
            P.prototype.__super = a.Dynamics.Joints.b2Joint.prototype;
            P.b2WeldJoint = function () {
                a.Dynamics.Joints.b2Joint.b2Joint.apply(this,
                arguments);
                this.m_localAnchorA = new f;
                this.m_localAnchorB = new f;
                this.m_impulse = new l;
                this.m_mass = new g
            };
            P.prototype.GetAnchorA = function () {
                return this.m_bodyA.GetWorldPoint(this.m_localAnchorA)
            };
            P.prototype.GetAnchorB = function () {
                return this.m_bodyB.GetWorldPoint(this.m_localAnchorB)
            };
            P.prototype.GetReactionForce = function (a) {
                a === void 0 && (a = 0);
                return new f(a * this.m_impulse.x, a * this.m_impulse.y)
            };
            P.prototype.GetReactionTorque = function (a) {
                a === void 0 && (a = 0);
                return a * this.m_impulse.z
            };
            P.prototype.b2WeldJoint = function (a) {
                this.__super.b2Joint.call(this, a);
                this.m_localAnchorA.SetV(a.localAnchorA);
                this.m_localAnchorB.SetV(a.localAnchorB);
                this.m_referenceAngle = a.referenceAngle;
                this.m_impulse.SetZero();
                this.m_mass = new g
            };
            P.prototype.InitVelocityConstraints = function (a) {
                var b, c = 0,
                    d = this.m_bodyA,
                    g = this.m_bodyB;
                b = d.m_xf.R;
                var f = this.m_localAnchorA.x - d.m_sweep.localCenter.x,
                    m = this.m_localAnchorA.y - d.m_sweep.localCenter.y,
                    c = b.col1.x * f + b.col2.x * m,
                    m = b.col1.y * f + b.col2.y * m,
                    f = c;
                b = g.m_xf.R;
                var e = this.m_localAnchorB.x - g.m_sweep.localCenter.x,
                    k = this.m_localAnchorB.y - g.m_sweep.localCenter.y,
                    c = b.col1.x * e + b.col2.x * k,
                    k = b.col1.y * e + b.col2.y * k,
                    e = c;
                b = d.m_invMass;
                var c = g.m_invMass,
                    l = d.m_invI,
                    p = g.m_invI;
                this.m_mass.col1.x = b + c + m * m * l + k * k * p;
                this.m_mass.col2.x = -m * f * l - k * e * p;
                this.m_mass.col3.x = -m * l - k * p;
                this.m_mass.col1.y = this.m_mass.col2.x;
                this.m_mass.col2.y = b + c + f * f * l + e * e * p;
                this.m_mass.col3.y = f * l + e * p;
                this.m_mass.col1.z = this.m_mass.col3.x;
                this.m_mass.col2.z = this.m_mass.col3.y;
                this.m_mass.col3.z = l + p;
                a.warmStarting ? (this.m_impulse.x *= a.dtRatio, this.m_impulse.y *= a.dtRatio, this.m_impulse.z *= a.dtRatio, d.m_linearVelocity.x -= b * this.m_impulse.x, d.m_linearVelocity.y -= b * this.m_impulse.y, d.m_angularVelocity -= l * (f * this.m_impulse.y - m * this.m_impulse.x + this.m_impulse.z), g.m_linearVelocity.x += c * this.m_impulse.x, g.m_linearVelocity.y += c * this.m_impulse.y, g.m_angularVelocity += p * (e * this.m_impulse.y - k * this.m_impulse.x + this.m_impulse.z)) : this.m_impulse.SetZero()
            };
            P.prototype.SolveVelocityConstraints = function () {
                var a, b = 0,
                    c = this.m_bodyA,
                    d = this.m_bodyB,
                    g = c.m_linearVelocity,
                    f = c.m_angularVelocity,
                    m = d.m_linearVelocity,
                    e = d.m_angularVelocity,
                    k = c.m_invMass,
                    p = d.m_invMass,
                    i = c.m_invI,
                    o = d.m_invI;
                a = c.m_xf.R;
                var r = this.m_localAnchorA.x - c.m_sweep.localCenter.x,
                    s = this.m_localAnchorA.y - c.m_sweep.localCenter.y,
                    b = a.col1.x * r + a.col2.x * s,
                    s = a.col1.y * r + a.col2.y * s,
                    r = b;
                a = d.m_xf.R;
                var t = this.m_localAnchorB.x - d.m_sweep.localCenter.x,
                    z = this.m_localAnchorB.y - d.m_sweep.localCenter.y,
                    b = a.col1.x * t + a.col2.x * z,
                    z = a.col1.y * t + a.col2.y * z,
                    t = b;
                a = m.x - e * z - g.x + f * s;
                var b = m.y + e * t - g.y - f * r,
                    A = e - f,
                    B = new l;
                this.m_mass.Solve33(B, -a, -b, -A);
                this.m_impulse.Add(B);
                g.x -= k * B.x;
                g.y -= k * B.y;
                f -= i * (r * B.y - s * B.x + B.z);
                m.x += p * B.x;
                m.y += p * B.y;
                e += o * (t * B.y - z * B.x + B.z);
                c.m_angularVelocity = f;
                d.m_angularVelocity = e
            };
            P.prototype.SolvePositionConstraints = function () {
                var a, c = 0,
                    d = this.m_bodyA,
                    g = this.m_bodyB;
                a = d.m_xf.R;
                var f = this.m_localAnchorA.x - d.m_sweep.localCenter.x,
                    m = this.m_localAnchorA.y - d.m_sweep.localCenter.y,
                    c = a.col1.x * f + a.col2.x * m,
                    m = a.col1.y * f + a.col2.y * m,
                    f = c;
                a = g.m_xf.R;
                var i = this.m_localAnchorB.x - g.m_sweep.localCenter.x,
                    e = this.m_localAnchorB.y - g.m_sweep.localCenter.y,
                    c = a.col1.x * i + a.col2.x * e,
                    e = a.col1.y * i + a.col2.y * e,
                    i = c;
                a = d.m_invMass;
                var c = g.m_invMass,
                    k = d.m_invI,
                    o = g.m_invI,
                    r = g.m_sweep.c.x + i - d.m_sweep.c.x - f,
                    s = g.m_sweep.c.y + e - d.m_sweep.c.y - m,
                    t = g.m_sweep.a - d.m_sweep.a - this.m_referenceAngle,
                    z = 10 * b.b2_linearSlop,
                    A = Math.sqrt(r * r + s * s),
                    B = p.Abs(t);
                A > z && (k *= 1, o *= 1);
                this.m_mass.col1.x = a + c + m * m * k + e * e * o;
                this.m_mass.col2.x = -m * f * k - e * i * o;
                this.m_mass.col3.x = -m * k - e * o;
                this.m_mass.col1.y = this.m_mass.col2.x;
                this.m_mass.col2.y = a + c + f * f * k + i * i * o;
                this.m_mass.col3.y = f * k + i * o;
                this.m_mass.col1.z = this.m_mass.col3.x;
                this.m_mass.col2.z = this.m_mass.col3.y;
                this.m_mass.col3.z = k + o;
                z = new l;
                this.m_mass.Solve33(z, -r, -s, -t);
                d.m_sweep.c.x -= a * z.x;
                d.m_sweep.c.y -= a * z.y;
                d.m_sweep.a -= k * (f * z.y - m * z.x + z.z);
                g.m_sweep.c.x += c * z.x;
                g.m_sweep.c.y += c * z.y;
                g.m_sweep.a += o * (i * z.y - e * z.x + z.z);
                d.SynchronizeTransform();
                g.SynchronizeTransform();
                return A <= b.b2_linearSlop && B <= b.b2_angularSlop
            };
            a.inherit(L, a.Dynamics.Joints.b2JointDef);
            L.prototype.__super = a.Dynamics.Joints.b2JointDef.prototype;
            L.b2WeldJointDef = function () {
                a.Dynamics.Joints.b2JointDef.b2JointDef.apply(this, arguments);
                this.localAnchorA = new f;
                this.localAnchorB = new f
            };
            L.prototype.b2WeldJointDef = function () {
                this.__super.b2JointDef.call(this);
                this.type = w.e_weldJoint;
                this.referenceAngle = 0
            };
            L.prototype.Initialize = function (a, b, c) {
                this.bodyA = a;
                this.bodyB = b;
                this.localAnchorA.SetV(this.bodyA.GetLocalPoint(c));
                this.localAnchorB.SetV(this.bodyB.GetLocalPoint(c));
                this.referenceAngle = this.bodyB.GetAngle() - this.bodyA.GetAngle()
            }
        })();
        (function () {
            var b = a.Dynamics.b2DebugDraw;
            b.b2DebugDraw = function () {
                this.m_xformScale = this.m_fillAlpha = this.m_alpha = this.m_lineThickness = this.m_drawScale = 1;
                var a = this;
                this.m_sprite = {
                    graphics: {
                        clear: function () {
                            a.m_ctx.clearRect(0, 0, a.m_ctx.canvas.width, a.m_ctx.canvas.height)
                        }
                    }
                }
            };
            b.prototype._color = function (a, b) {
                return "rgba(" + ((a & 16711680) >> 16) + "," + ((a & 65280) >> 8) + "," + (a & 255) + "," + b + ")"
            };
            b.prototype.b2DebugDraw = function () {
                this.m_drawFlags = 0
            };
            b.prototype.SetFlags = function (a) {
                a === void 0 && (a = 0);
                this.m_drawFlags = a
            };
            b.prototype.GetFlags = function () {
                return this.m_drawFlags
            };
            b.prototype.AppendFlags = function (a) {
                a === void 0 && (a = 0);
                this.m_drawFlags |= a
            };
            b.prototype.ClearFlags = function (a) {
                a === void 0 && (a = 0);
                this.m_drawFlags &= ~a
            };
            b.prototype.SetSprite = function (a) {
                this.m_ctx = a
            };
            b.prototype.GetSprite = function () {
                return this.m_ctx
            };
            b.prototype.SetDrawScale = function (a) {
                a === void 0 && (a = 0);
                this.m_drawScale = a
            };
            b.prototype.GetDrawScale = function () {
                return this.m_drawScale
            };
            b.prototype.SetLineThickness = function (a) {
                a === void 0 && (a = 0);
                this.m_lineThickness = a;
                this.m_ctx.strokeWidth = a
            };
            b.prototype.GetLineThickness = function () {
                return this.m_lineThickness
            };
            b.prototype.SetAlpha = function (a) {
                a === void 0 && (a = 0);
                this.m_alpha = a
            };
            b.prototype.GetAlpha = function () {
                return this.m_alpha
            };
            b.prototype.SetFillAlpha = function (a) {
                a === void 0 && (a = 0);
                this.m_fillAlpha = a
            };
            b.prototype.GetFillAlpha = function () {
                return this.m_fillAlpha
            };
            b.prototype.SetXFormScale = function (a) {
                a === void 0 && (a = 0);
                this.m_xformScale = a
            };
            b.prototype.GetXFormScale = function () {
                return this.m_xformScale
            };
            b.prototype.DrawPolygon = function (a, b, m) {
                if (b) {
                    var f = this.m_ctx,
                        l = this.m_drawScale;
                    f.beginPath();
                    f.strokeStyle = this._color(m.color, this.m_alpha);
                    f.moveTo(a[0].x * l, a[0].y * l);
                    for (m = 1; m < b; m++) f.lineTo(a[m].x * l, a[m].y * l);
                    f.lineTo(a[0].x * l, a[0].y * l);
                    f.closePath();
                    f.stroke()
                }
            };
            b.prototype.DrawSolidPolygon = function (a, b, m) {
                if (b) {
                    var f = this.m_ctx,
                        l = this.m_drawScale;
                    f.beginPath();
                    f.strokeStyle = this._color(m.color, this.m_alpha);
                    f.fillStyle = this._color(m.color, this.m_fillAlpha);
                    f.moveTo(a[0].x * l, a[0].y * l);
                    for (m = 1; m < b; m++) f.lineTo(a[m].x * l, a[m].y * l);
                    f.lineTo(a[0].x * l, a[0].y * l);
                    f.closePath();
                    f.fill();
                    f.stroke()
                }
            };
            b.prototype.DrawCircle = function (a, b, m) {
                if (b) {
                    var f = this.m_ctx,
                        l = this.m_drawScale;
                    f.beginPath();
                    f.strokeStyle = this._color(m.color, this.m_alpha);
                    f.arc(a.x * l, a.y * l, b * l, 0, Math.PI * 2, !0);
                    f.closePath();
                    f.stroke()
                }
            };
            b.prototype.DrawSolidCircle = function (a, b, m, f) {
                if (b) {
                    var l = this.m_ctx,
                        i = this.m_drawScale,
                        d = a.x * i,
                        r = a.y * i;
                    l.moveTo(0, 0);
                    l.beginPath();
                    l.strokeStyle = this._color(f.color, this.m_alpha);
                    l.fillStyle = this._color(f.color, this.m_fillAlpha);
                    l.arc(d, r, b * i, 0, Math.PI * 2, !0);
                    l.moveTo(d, r);
                    l.lineTo((a.x + m.x * b) * i, (a.y + m.y * b) * i);
                    l.closePath();
                    l.fill();
                    l.stroke()
                }
            };
            b.prototype.DrawSegment = function (a, b, m) {
                var f = this.m_ctx,
                    l = this.m_drawScale;
                f.strokeStyle = this._color(m.color, this.m_alpha);
                f.beginPath();
                f.moveTo(a.x * l, a.y * l);
                f.lineTo(b.x * l, b.y * l);
                f.closePath();
                f.stroke()
            };
            b.prototype.DrawTransform = function (a) {
                var b = this.m_ctx,
                    m = this.m_drawScale;
                b.beginPath();
                b.strokeStyle = this._color(16711680, this.m_alpha);
                b.moveTo(a.position.x * m, a.position.y * m);
                b.lineTo((a.position.x + this.m_xformScale * a.R.col1.x) * m, (a.position.y + this.m_xformScale * a.R.col1.y) * m);
                b.strokeStyle = this._color(65280, this.m_alpha);
                b.moveTo(a.position.x * m, a.position.y * m);
                b.lineTo((a.position.x + this.m_xformScale * a.R.col2.x) * m, (a.position.y + this.m_xformScale * a.R.col2.y) * m);
                b.closePath();
                b.stroke()
            }
        })();
        var m;
        for (m = 0; m < a.postDefs.length; ++m) a.postDefs[m]();
        delete a.postDefs;
        i.Box2D = a;
        i.b2AABB = a.Collision.b2AABB;
        i.b2Vec2 = a.Common.Math.b2Vec2;
        i.b2BodyDef = a.Dynamics.b2BodyDef;
        i.b2Body = a.Dynamics.b2Body;
        i.b2FixtureDef = a.Dynamics.b2FixtureDef;
        i.b2Fixture = a.Dynamics.b2Fixture;
        i.b2World = a.Dynamics.b2World;
        i.b2MassData = a.Collision.Shapes.b2MassData;
        i.b2PolygonShape = a.Collision.Shapes.b2PolygonShape;
        i.b2CircleShape = a.Collision.Shapes.b2CircleShape;
        i.b2DebugDraw = a.Dynamics.b2DebugDraw;
        i.b2RevoluteJointDef = a.Dynamics.Joints.b2RevoluteJointDef;
        i.b2PrismaticJointDef = a.Dynamics.Joints.b2PrismaticJointDef;
        i.b2Transform = a.Common.Math.b2Transform
    }
}, []);
require.define({
    "gamejs/display": function (y, i) {
        var a = y("../gamejs").Surface,
            s = null;
        i.init = function () {
            var a = null;
            m() === null && (a = document.createElement("canvas"), a.setAttribute("id", "gjs-canvas"), document.body.appendChild(a));
            (a = document.getElementById("gjs-loader")) && a.parentNode.removeChild(a)
        };
        i.setMode = function (a) {
            var c = m();
            c.width = a[0];
            c.height = a[1];
            return r()
        };
        i.setCaption = function (a) {
            document.title = a
        };
        i._getCanvasOffset = function () {
            var a = m().getBoundingClientRect();
            return [a.left, a.top]
        };
        var r = i.getSurface = function () {
            if (s === null) {
                var b = m();
                s = new a([b.clientWidth, b.clientHeight]);
                s._canvas = b
            }
            return s
        }, m = function () {
            var a = null;
            Array.prototype.slice.call(document.getElementsByTagName("canvas")).every(function (c) {
                if (c.getAttribute("id") == "gjs-canvas") return a = c, !1;
                return !0
            });
            return a
        }
    }
}, ["gamejs"]);
require.define({
    "gamejs/draw": function (y, i) {
        i.line = function (a, i, r, m, b) {
            a = a.context;
            a.save();
            a.beginPath();
            a.strokeStyle = i;
            a.lineWidth = b || 1;
            a.moveTo(r[0], r[1]);
            a.lineTo(m[0], m[1]);
            a.stroke();
            a.restore()
        };
        i.lines = function (a, i, r, m, b) {
            r = r || !1;
            a = a.context;
            a.save();
            a.beginPath();
            a.strokeStyle = a.fillStyle = i;
            a.lineWidth = b || 1;
            for (i = 0; i < m.length; i++) b = m[i], i === 0 ? a.moveTo(b[0], b[1]) : a.lineTo(b[0], b[1]);
            r && a.lineTo(m[0][0], m[0][1]);
            a.stroke();
            a.restore()
        };
        i.circle = function (a, i, r, m, b) {
            if (!m) throw Error("[circle] radius required argument");
            if (!r || typeof r !== "array") throw Error("[circle] pos must be given & array" + r);
            a = a.context;
            a.save();
            a.beginPath();
            a.strokeStyle = a.fillStyle = i;
            a.lineWidth = b || 1;
            a.arc(r[0], r[1], m, 0, 2 * Math.PI, !0);
            b === void 0 || b === 0 ? a.fill() : a.stroke();
            a.restore()
        };
        i.rect = function (a, i, r, m) {
            a = a.context;
            a.save();
            a.strokeStyle = a.fillStyle = i;
            isNaN(m) || m === 0 ? a.fillRect(r.left, r.top, r.width, r.height) : (a.lineWidth = m, a.strokeRect(r.left, r.top, r.width, r.height));
            a.restore()
        };
        i.arc = function (a, i, r, m, b, c) {
            a = a.context;
            a.save();
            a.strokeStyle = a.fillStyle = i;
            a.arc(r.center[0], r.center[1], r.width / 2, m * (Math.PI / 180), b * (Math.PI / 180), !1);
            isNaN(c) || c === 0 ? a.fill() : (a.lineWidth = c, a.stroke());
            a.restore()
        };
        i.polygon = function (a, i, r, m) {
            this.lines(a, i, !0, r, m)
        }
    }
}, []);
require.define({
    "gamejs/event": function (y, i) {
        var a = y("./display"),
            s = y("../gamejs");
        i.K_UP = 38;
        i.K_DOWN = 40;
        i.K_RIGHT = 39;
        i.K_LEFT = 37;
        i.K_SPACE = 32;
        i.K_BACKSPACE = 8;
        i.K_TAB = 9;
        i.K_ENTER = 13;
        i.K_SHIFT = 16;
        i.K_CTRL = 17;
        i.K_ALT = 18;
        i.K_ESC = 27;
        i.K_0 = 48;
        i.K_1 = 49;
        i.K_2 = 50;
        i.K_3 = 51;
        i.K_4 = 52;
        i.K_5 = 53;
        i.K_6 = 54;
        i.K_7 = 55;
        i.K_8 = 56;
        i.K_9 = 57;
        i.K_a = 65;
        i.K_b = 66;
        i.K_c = 67;
        i.K_d = 68;
        i.K_e = 69;
        i.K_f = 70;
        i.K_g = 71;
        i.K_h = 72;
        i.K_i = 73;
        i.K_j = 74;
        i.K_k = 75;
        i.K_l = 76;
        i.K_m = 77;
        i.K_n = 78;
        i.K_o = 79;
        i.K_p = 80;
        i.K_q = 81;
        i.K_r = 82;
        i.K_s = 83;
        i.K_t = 84;
        i.K_u = 85;
        i.K_v = 86;
        i.K_w = 87;
        i.K_x = 88;
        i.K_y = 89;
        i.K_z = 90;
        i.K_KP1 = 97;
        i.K_KP2 = 98;
        i.K_KP3 = 99;
        i.K_KP4 = 100;
        i.K_KP5 = 101;
        i.K_KP6 = 102;
        i.K_KP7 = 103;
        i.K_KP8 = 104;
        i.K_KP9 = 105;
        i.QUIT = 0;
        i.KEY_DOWN = 1;
        i.KEY_UP = 2;
        i.MOUSE_MOTION = 3;
        i.MOUSE_UP = 4;
        i.MOUSE_DOWN = 5;
        i.MOUSE_WHEEL = 6;
        var r = [];
        i.get = function () {
            return r.splice(0, r.length)
        };
        i.poll = function () {
            return r.pop()
        };
        i.post = function (a) {
            r.push(a)
        };
        i.Event = function () {
            this.pos = this.button = this.rel = this.key = this.type = null
        };
        i.init = function () {
            function m(b) {
                var g = a._getCanvasOffset();
                r.push({
                    type: s.event.MOUSE_WHEEL,
                    pos: [b.clientX - g[0], b.clientY - g[1]],
                    delta: b.detail || -b.wheelDeltaY / 40
                })
            }
            document.addEventListener("mousedown", function (b) {
                var g = a._getCanvasOffset();
                r.push({
                    type: s.event.MOUSE_DOWN,
                    pos: [b.clientX - g[0], b.clientY - g[1]],
                    button: b.button,
                    shiftKey: b.shiftKey,
                    ctrlKey: b.ctrlKey,
                    metaKey: b.metaKey
                })
            }, !1);
            document.addEventListener("mouseup", function (b) {
                var g = a._getCanvasOffset();
                r.push({
                    type: s.event.MOUSE_UP,
                    pos: [b.clientX - g[0], b.clientY - g[1]],
                    button: b.button,
                    shiftKey: b.shiftKey,
                    ctrlKey: b.ctrlKey,
                    metaKey: b.metaKey
                })
            }, !1);
            document.addEventListener("keydown", function (a) {
                var b = a.keyCode || a.which;
                r.push({
                    type: s.event.KEY_DOWN,
                    key: b,
                    shiftKey: a.shiftKey,
                    ctrlKey: a.ctrlKey,
                    metaKey: a.metaKey
                });
                (!a.ctrlKey && !a.metaKey && (b >= i.K_LEFT && b <= i.K_DOWN || b >= i.K_0 && b <= i.K_z || b >= i.K_KP1 && b <= i.K_KP9 || b === i.K_SPACE || b === i.K_TAB || b === i.K_ENTER) || b === i.K_ALT || b === i.K_BACKSPACE) && a.preventDefault()
            }, !1);
            document.addEventListener("keyup", function (a) {
                r.push({
                    type: s.event.KEY_UP,
                    key: a.keyCode,
                    shiftKey: a.shiftKey,
                    ctrlKey: a.ctrlKey,
                    metaKey: a.metaKey
                })
            }, !1);
            var b = [];
            document.addEventListener("mousemove", function (c) {
                var g = a._getCanvasOffset(),
                    g = [c.clientX - g[0], c.clientY - g[1]],
                    m = [];
                b.length && (m = [b[0] - g[0], b[1] - g[1]]);
                r.push({
                    type: s.event.MOUSE_MOTION,
                    pos: g,
                    rel: m,
                    buttons: null,
                    timestamp: c.timeStamp
                });
                b = g
            }, !1);
            document.addEventListener("mousewheel", m, !1);
            document.addEventListener("DOMMouseScroll", m, !1);
            document.addEventListener("beforeunload", function () {
                r.push({
                    type: s.event.QUIT
                })
            }, !1)
        }
    }
}, ["gamejs/display", "gamejs"]);
require.define({
    "gamejs/font": function (y, i) {
        var a = y("../gamejs").Surface,
            s = y("./utils/objects"),
            r = i.Font = function (m) {
                this.sampleSurface = new a([10, 10]);
                this.sampleSurface.context.font = m;
                this.sampleSurface.context.textAlign = "start";
                this.sampleSurface.context.textBaseline = "bottom";
                return this
            };
        r.prototype.render = function (m, b) {
            var c = this.size(m),
                c = new a(c),
                g = c.context;
            g.save();
            g.font = this.sampleSurface.context.font;
            g.textBaseline = this.sampleSurface.context.textBaseline;
            g.textAlign = this.sampleSurface.context.textAlign;
            g.fillStyle = g.strokeStyle = b || "#000000";
            g.fillText(m, 0, c.rect.height, c.rect.width);
            g.restore();
            return c
        };
        r.prototype.size = function (a) {
            return [this.sampleSurface.context.measureText(a).width, this.fontHeight]
        };
        s.accessors(r.prototype, {
            fontHeight: {
                get: function () {
                    return this.sampleSurface.context.measureText("M").width * 1.5
                }
            }
        })
    }
}, ["gamejs", "gamejs/utils/objects"]);
require.define({
    "gamejs/http": function (y, i) {
        function a(a) {
            return eval("(" + a.responseText + ")")
        }
        i.Response = function () {
            this.getResponseHeader = function () {};
            throw Error("response class not instantiable");
        };
        var s = i.ajax = function (a, c, g, m) {
            var g = g || null,
                f = new XMLHttpRequest;
            f.open(a, c, !1);
            m && f.setRequestHeader("Accept", m);
            g instanceof Object && (g = JSON.stringify(g), f.setRequestHeader("Content-Type", "application/json"));
            f.setRequestHeader("X-Requested-With", "XMLHttpRequest");
            f.send(g);
            return f
        }, r = i.get = function (a) {
            return s("GET",
            a)
        }, m = i.post = function (a, c, g) {
            return s("POST", a, c, g)
        };
        i.load = function (b) {
            return a(r((window.$g && $g.ajaxBaseHref || "./") + b))
        };
        i.save = function (b, c, g) {
            return a(m((window.$g && $g.ajaxBaseHref || "./") + b, {
                payload: c
            }, g))
        }
    }
}, []);
require.define({
    "gamejs/image": function (y, i) {
        var a = y("../gamejs"),
            s = {};
        i.load = function (i) {
            var m;
            if (typeof i === "string") {
                if (m = s[i], !m) throw Error('Missing "' + i + '", gamejs.preload() all images before trying to load them.');
            } else m = i;
            i = document.createElement("canvas");
            i.width = m.naturalWidth || m.width;
            i.height = m.naturalHeight || m.height;
            i.getContext("2d").drawImage(m, 0, 0);
            m.getSize = function () {
                return [m.naturalWidth, m.naturalHeight]
            };
            var b = new a.Surface(m.getSize());
            b._canvas = i;
            return b
        };
        i.init = function () {};
        i.preload = function (i) {
            function m() {
                g++;
                g % 10 === 0 && a.log("gamejs.image: preloaded  " + g + " of " + p)
            }
            function b() {
                s[this.gamejsKey] = this;
                m()
            }
            function c() {
                m();
                throw Error("Error loading " + this.src);
            }
            var g = 0,
                p = 0,
                f;
            for (f in i) if (!(f.indexOf("png") == -1 && f.indexOf("jpg") == -1 && f.indexOf("gif") == -1)) {
                var l = new Image;
                l.addEventListener("load", b, !0);
                l.addEventListener("error", c, !0);
                l.src = i[f];
                l.gamejsKey = f;
                p++
            }
            return function () {
                return p > 0 ? g / p : 1
            }
        }
    }
}, ["gamejs"]);
require.define({
    "gamejs/mask": function (y, i) {
        var a = y("../gamejs"),
            s = y("./utils/objects");
        i.fromSurface = function (a, b) {
            for (var b = b && 255 - b || 255, c = a.getImageData(), g = a.getSize(), i = new r(g), f = 0; f < c.length; f += 4) {
                var l = parseInt(f / 4 / g[0], 10),
                    s = parseInt(f / 4 % g[0], 10);
                c[f + 3] >= b && i.setAt(s, l)
            }
            return i
        };
        var r = i.Mask = function (a) {
            this.width = a[0];
            this.height = a[1];
            this._bits = [];
            for (a = 0; a < this.width; a++) {
                this._bits[a] = [];
                for (var b = 0; b < this.height; b++) this._bits[a][b] = !1
            }
        };
        r.prototype.overlapRect = function (m, b) {
            var c = this.rect,
                g = m.rect;
            b && g.moveIp(b);
            if (!g.collideRect(c)) return null;
            var i = Math.max(c.left, g.left),
                f = Math.max(c.top, g.top);
            return new a.Rect([i, f], [Math.min(c.right, g.right) - i, Math.min(c.bottom, g.bottom) - f])
        };
        r.prototype.overlap = function (a, b) {
            var c = this.overlapRect(a, b);
            if (c === null) return !1;
            var g = this.rect,
                i = a.rect;
            b && i.moveIp(b);
            for (var f = c.top; f <= c.bottom; f++) for (var l = c.left; l <= c.right; l++) if (this.getAt(l - g.left, f - g.top) && a.getAt(l - i.left, f - i.top)) return !0;
            return !1
        };
        r.prototype.overlapArea = function (a,
        b) {
            var c = this.overlapRect(a, b);
            if (c === null) return 0;
            var g = this.rect,
                i = a.rect;
            b && i.moveIp(b);
            for (var f = 0, l = c.top; l <= c.bottom; l++) for (var r = c.left; r <= c.right; r++) this.getAt(r - g.left, l - g.top) && a.getAt(r - i.left, l - i.top) && f++;
            return f
        };
        r.prototype.overlapMask = function (a, b) {
            var c = this.overlapRect(a, b);
            if (c === null) return 0;
            var g = this.rect,
                i = a.rect;
            b && i.moveIp(b);
            for (var f = new r([c.width, c.height]), l = c.top; l <= c.bottom; l++) for (var s = c.left; s <= c.right; s++) this.getAt(s - g.left, l - g.top) && a.getAt(s - i.left, l - i.top) && f.setAt(s, l);
            return f
        };
        r.prototype.setAt = function (a, b) {
            this._bits[a][b] = !0
        };
        r.prototype.getAt = function (a, b) {
            a = parseInt(a, 10);
            b = parseInt(b, 10);
            if (a < 0 || b < 0 || a >= this.width || b >= this.height) return !1;
            return this._bits[a][b]
        };
        r.prototype.invert = function () {
            this._bits = this._bits.map(function (a) {
                return a.map(function (a) {
                    return !a
                })
            })
        };
        r.prototype.getSize = function () {
            return [this.width, this.height]
        };
        s.accessors(r.prototype, {
            rect: {
                get: function () {
                    return new a.Rect([0, 0], [this.width, this.height])
                }
            },
            length: {
                get: function () {
                    var a = 0;
                    this._bits.forEach(function (b) {
                        b.forEach(function (b) {
                            b && a++
                        })
                    });
                    return a
                }
            }
        })
    }
}, ["gamejs", "gamejs/utils/objects"]);
require.define({
    "gamejs/mixer": function (y, i) {
        function a(a) {
            a instanceof Array || (a = [a]);
            a.forEach(function (a) {
                s[a.gamejsKey] = a
            })
        }
        y("../gamejs");
        var s = {}, r = !1;
        i.init = function () {
            var m = Array.prototype.slice.call(document.getElementsByTagName("audio"), 0);
            a(m)
        };
        i.preload = function (m) {
            function b() {
                a(this);
                i++;
                i == g && (r = !1)
            }
            function c() {
                i++;
                i == g && (r = !1);
                throw Error("Error loading " + this.src);
            }
            var g = 0,
                i = 0,
                f;
            for (f in m) if (!(f.indexOf("wav") == -1 && f.indexOf("ogg") == -1 && f.indexOf("webm") == -1)) {
                g++;
                var l = new Audio;
                l.addEventListener("canplay", b, !0);
                l.addEventListener("error", c, !0);
                l.src = m[f];
                l.gamejsKey = f;
                l.load()
            }
            g > 0 && (r = !0);
            return function () {
                return g > 0 ? i / g : 1
            }
        };
        i.isPreloading = function () {
            return r
        };
        i.Sound = function (a) {
            var b;
            b = typeof a === "string" ? s[a] : a;
            if (!b) throw Error('Missing "' + a + '", gamejs.preload() all audio files before loading');
            var c = new Audio;
            c.preload = "auto";
            c.loop = "false";
            c.src = b.src;
            this.play = function () {
                (c.ended || c.paused) && c.play()
            };
            this.stop = function () {
                c.pause()
            };
            this.setVolume = function (a) {
                c.volume = a
            };
            this.getVolume = function () {
                return c.volume
            };
            this.getLength = function () {
                return c.duration
            };
            return this
        }
    }
}, ["gamejs"]);
require.define({
    "gamejs/pathfinding/astar": function (y, i) {
        function a() {
            var a = {};
            this.store = function (b, c) {
                a[b[0] + "-" + b[1]] = c
            };
            this.find = function (b) {
                return a[b[0] + "-" + b[1]]
            };
            return this
        }
        var s = y("../utils/binaryheap").BinaryHeap;
        i.findRoute = function (m, b, c, g) {
            function i(a) {
                l.push(a);
                r.store(a.point, a)
            }
            function f(a) {
                var b = r.find(a),
                    c = d.length + m.actualDistance(d.point, a);
                if (!b || b.length > c) b && l.remove(b), i({
                    point: a,
                    from: d,
                    length: c
                })
            }
            var l = new s(function (a) {
                if (a.score === void 0) a.score = m.estimatedDistance(a.point,
                c) + a.length;
                return a.score
            }),
                r = new a;
            i({
                point: b,
                from: null,
                length: 0
            });
            for (var b = Date.now(), d = null; l.size() > 0 && (!g || Date.now() - b < g);) {
                d = l.pop();
                if (c[0] === d.point[0] && c[1] === d.point[1]) return d;
                m.adjacent(d.point).forEach(f)
            }
            return null
        };
        var r = i.Map = function () {
            throw Error("not instantiable, this is an interface");
        };
        r.prototype.adjacent = function () {};
        r.prototype.estimatedDistance = function () {};
        r.prototype.actualDistance = function () {}
    }
}, ["gamejs/utils/binaryheap"]);
require.define({
    "gamejs/sprite": function (y, i) {
        y("../gamejs");
        var a = y("./utils/arrays"),
            s = i.Sprite = function () {
                this._groups = [];
                this._alive = !0;
                this.rect = this.image = null;
                return this
            };
        s.prototype.kill = function () {
            this._alive = !1;
            this._groups.forEach(function (a) {
                a.remove(this)
            }, this)
        };
        s.prototype.remove = function (a) {
            a instanceof Array || (a = [a]);
            a.forEach(function (a) {
                a.remove(this)
            }, this)
        };
        s.prototype.add = function (a) {
            a instanceof Array || (a = [a]);
            a.forEach(function (a) {
                a.add(this)
            }, this)
        };
        s.prototype.draw = function (a) {
            a.blit(this.image,
            this.rect)
        };
        s.prototype.update = function () {};
        s.prototype.isDead = function () {
            return !this._alive
        };
        var r = i.Group = function (a) {
            this._sprites = [];
            (a instanceof s || a instanceof Array && a.length && a[0] instanceof s) && this.add(a);
            return this
        };
        r.prototype.update = function () {
            var a = arguments;
            this._sprites.forEach(function (c) {
                c.update.apply(c, a)
            }, this)
        };
        r.prototype.add = function (a) {
            a instanceof Array || (a = [a]);
            a.forEach(function (a) {
                this._sprites.push(a);
                a._groups.push(this)
            }, this)
        };
        r.prototype.remove = function (b) {
            b instanceof
            Array || (b = [b]);
            b.forEach(function (b) {
                a.remove(b, this._sprites);
                a.remove(this, b._groups)
            }, this)
        };
        r.prototype.has = function (a) {
            a instanceof Array || (a = [a]);
            return a.every(function (a) {
                return this._sprites.indexOf(a) !== -1
            }, this)
        };
        r.prototype.sprites = function () {
            return this._sprites
        };
        r.prototype.draw = function () {
            var a = arguments;
            this._sprites.forEach(function (c) {
                c.draw.apply(c, a)
            }, this)
        };
        r.prototype.empty = function () {
            this._sprites = []
        };
        r.prototype.collidePoint = function () {
            var a = Array.prototype.slice.apply(arguments);
            return this._sprites.filter(function (c) {
                return c.rect.collidePoint.apply(c.rect, a)
            }, this)
        };
        r.prototype.forEach = function () {
            Array.prototype.forEach.apply(this._sprites, arguments)
        };
        r.prototype.some = function () {
            return Array.prototype.some.apply(this._sprites, arguments)
        };
        i.spriteCollide = function (a, c, g, i) {
            var i = i || m,
                g = g || !1,
                f = [];
            c.sprites().forEach(function (c) {
                i(a, c) && (g && c.kill(), f.push(c))
            });
            return f
        };
        i.groupCollide = function (a, c, g, i, f) {
            var g = g || !1,
                i = i || !1,
                l = [],
                r = f || m;
            a.sprites().forEach(function (a) {
                c.sprites().forEach(function (b) {
                    r(a,
                    b) && (g && a.kill(), i && b.kill(), l.push({
                        a: a,
                        b: b
                    }))
                })
            });
            return l
        };
        var m = i.collideRect = function (a, c) {
            return a.rect.collideRect(c.rect)
        };
        i.collideMask = function (a, c) {
            if (!a.mask || !c.mask) throw Error("Both sprites must have 'mask' attribute set to an gamejs.mask.Mask");
            return a.mask.overlap(c.mask, [c.rect.left - a.rect.left, c.rect.top - a.rect.top])
        };
        i.collideCircle = function (a, c) {
            return a.rect.center[0] * a.rect.center[0] + a.rect.center[1] * a.rect.center[1] <= (a.radius * a.radius || (a.rect.width * a.rect.width + a.rect.height * a.rect.height) / 4) + (c.radius * c.radius || (c.rect.width * c.rect.width + c.rect.height * c.rect.height) / 4)
        }
    }
}, ["gamejs", "gamejs/utils/arrays"]);
require.define({
    "gamejs/surfacearray": function (y, i) {
        var a = y("../gamejs"),
            s = y("./utils/objects").accessors;
        i.blitArray = function (a, m) {
            a.context.putImageData(m.imageData, 0, 0)
        };
        i.SurfaceArray = function (i) {
            this.set = function (a, c, f) {
                a = a * 4 + c * m[0] * 4;
                b[a] = f[0];
                b[a + 1] = f[1];
                b[a + 2] = f[2];
                b[a + 3] = f[3] === void 0 ? 255 : f[3]
            };
            this.get = function (a, c) {
                var f = a * 4 + c * m[0] * 4;
                return [b[f], b[f + 1], b[f + 2], b[f + 3]]
            };
            this.surface = null;
            s(this, {
                surface: {
                    get: function () {
                        var b = new a.Surface(m);
                        b.context.putImageData(c, 0, 0);
                        return b
                    }
                },
                imageData: {
                    get: function () {
                        return c
                    }
                }
            });
            this.getSize = function () {
                return m
            };
            var m = null,
                b = null,
                c = null;
            i instanceof Array ? (m = i, c = a.display.getSurface().context.createImageData(m[0], m[1]), b = c.data) : (m = i.getSize(), b = c = i.getImageData(0, 0, m[0], m[1]));
            return this
        }
    }
}, ["gamejs", "gamejs/utils/objects"]);
require.define({
    "gamejs/time": function (y, i) {
        var a = {}, s = {};
        i.init = function () {
            Date.now();
            setInterval(r, 10)
        };
        i.fpsCallback = function (m, b, c) {
            c = parseInt(1E3 / c, 10);
            a[c] = a[c] || [];
            s[c] = s[c] || 0;
            a[c].push({
                rawFn: m,
                callback: function (a) {
                    m.apply(b, [a])
                }
            })
        };
        i.deleteCallback = function (m, b) {
            var b = parseInt(1E3 / b, 10),
                c = a[b];
            c && (a[b] = c.filter(function (a) {
                if (a.rawFn !== m) return !0;
                return !1
            }))
        };
        var r = function () {
            function m(a) {
                a.callback(g)
            }
            var b = Date.now(),
                c;
            for (c in s) {
                s[c] || (s[c] = b);
                var g = b - s[c];
                c <= g && (s[c] = b, a[c].forEach(m,
                this))
            }
        }
    }
}, []);
require.define({
    "gamejs/transform": function (y, i) {
        var a = y("../gamejs").Surface,
            s = y("./utils/matrix");
        i.rotate = function (i, m) {
            var b = i.getSize(),
                c = new a(b),
                g = i._matrix;
            i._matrix = s.translate(i._matrix, b[0] / 2, b[1] / 2);
            i._matrix = s.rotate(i._matrix, m * Math.PI / 180);
            i._matrix = s.translate(i._matrix, -b[0] / 2, -b[1] / 2);
            c.blit(i);
            i._matrix = g;
            return c
        };
        i.scale = function (i, m) {
            var b = m[0],
                c = m[1],
                g = i.getSize(),
                g = [g[0] * m[0], g[1] * m[1]],
                g = new a(g);
            g._matrix = s.scale(g._matrix, [b, c]);
            g.blit(i);
            return g
        };
        i.flip = function (i, m, b) {
            var c = i.getSize(),
                g = new a(c),
                p = 1,
                f = 1,
                l = 0,
                s = 0;
            m === !0 && (p = -1, l = -c[0]);
            b === !0 && (f = -1, s = -c[1]);
            g.context.save();
            g.context.scale(p, f);
            g.context.drawImage(i.canvas, l, s);
            g.context.restore();
            return g
        }
    }
}, ["gamejs", "gamejs/utils/matrix"]);
require.define({
    "gamejs/utils/arrays": function (y, i) {
        i.remove = function (a, i) {
            return i.splice(i.indexOf(a), 1)
        }
    }
}, []);
require.define({
    "gamejs/utils/binaryheap": function (y, i) {
        var a = i.BinaryHeap = function (a) {
            this.content = [];
            this.scoreFunction = a;
            return this
        };
        a.prototype.push = function (a) {
            this.content.push(a);
            this.sinkDown(this.content.length - 1)
        };
        a.prototype.pop = function () {
            var a = this.content[0],
                i = this.content.pop();
            this.content.length > 0 && (this.content[0] = i, this.bubbleUp(0));
            return a
        };
        a.prototype.remove = function (a) {
            if (!this.content.some(function (i, m) {
                if (i == a) {
                    var b = this.content.pop();
                    m != this.content.length && (this.content[m] = b, this.scoreFunction(b) < this.scoreFunction(a) ? this.sinkDown(m) : this.bubbleUp(m));
                    return !0
                }
                return !1
            }, this)) throw Error("Node not found.");
        };
        a.prototype.size = function () {
            return this.content.length
        };
        a.prototype.sinkDown = function (a) {
            for (var i = this.content[a]; a > 0;) {
                var m = Math.floor((a + 1) / 2) - 1,
                    b = this.content[m];
                if (this.scoreFunction(i) < this.scoreFunction(b)) this.content[m] = i, this.content[a] = b, a = m;
                else break
            }
        };
        a.prototype.bubbleUp = function (a) {
            for (var i = this.content.length, m = this.content[a], b = this.scoreFunction(m);;) {
                var c = (a + 1) * 2,
                    g = c - 1,
                    p = null;
                if (g < i) {
                    var f = this.scoreFunction(this.content[g]);
                    f < b && (p = g)
                }
                if (c < i && this.scoreFunction(this.content[c]) < (p === null ? b : f)) p = c;
                if (p !== null) this.content[a] = this.content[p], this.content[p] = m, a = p;
                else break
            }
        }
    }
}, []);
require.define({
    "gamejs/utils/math": function (y, i) {
        i.normaliseDegrees = function (a) {
            a %= 360;
            a < 0 && (a += 360);
            return a
        };
        i.normaliseRadians = function (a) {
            a %= 2 * Math.PI;
            a < 0 && (a += 2 * Math.PI);
            return a
        };
        i.degrees = function (a) {
            return a * (180 / Math.PI)
        };
        i.radians = function (a) {
            return a * (Math.PI / 180)
        }
    }
}, []);
require.define({
    "gamejs/utils/matrix": function (y, i) {
        i.identity = function () {
            return [1, 0, 0, 1, 0, 0]
        };
        i.add = function (a, i) {
            return [a[0] + i[0], a[1] + i[1], a[2] + i[2], a[3] + i[3], a[4] + i[4], a[5] + i[5], a[6] + i[6]]
        };
        var a = i.multiply = function (a, i) {
            return [a[0] * i[0] + a[2] * i[1], a[1] * i[0] + a[3] * i[1], a[0] * i[2] + a[2] * i[3], a[1] * i[2] + a[3] * i[3], a[0] * i[4] + a[2] * i[5] + a[4], a[1] * i[4] + a[3] * i[5] + a[5]]
        };
        i.translate = function (i, r, m) {
            return a(i, [1, 0, 0, 1, r, m])
        };
        i.rotate = function (i, r) {
            var m = Math.sin(r),
                b = Math.cos(r);
            return a(i, [b, m, -m, b, 0, 0])
        };
        i.rotation = function (a) {
            return Math.atan2(a[1], a[0])
        };
        i.scale = function (i, r) {
            return a(i, [r[0], 0, 0, r[1], 0, 0])
        }
    }
}, []);
require.define({
    "gamejs/utils/objects": function (y, i) {
        i.extend = function (a, i) {
            if (a === void 0) throw Error("unknown subClass");
            if (i === void 0) throw Error("unknown superClass");
            var b = new Function;
            b.prototype = i.prototype;
            a.prototype = new b;
            a.prototype.constructor = a;
            a.superClass = i.prototype;
            a.superConstructor = i
        };
        i.merge = function () {
            for (var a = {}, i = arguments.length; i > 0; --i) {
                var b = arguments[i - 1],
                    c;
                for (c in b) a[c] = b[c]
            }
            return a
        };
        var a = i.keys = function (a) {
            if (Object.keys) return Object.keys(a);
            var i = [],
                b;
            for (b in a) Object.prototype.hasOwnProperty.call(a,
            b) && i.push(b);
            return i
        }, s = i.accessor = function (a, i, b, c) {
            Object.defineProperty !== void 0 ? Object.defineProperty(a, i, {
                get: b,
                set: c
            }) : Object.prototype.__defineGetter__ !== void 0 && (a.__defineGetter__(i, b), c && a.__defineSetter__(i, c))
        };
        i.accessors = function (i, m) {
            a(m).forEach(function (a) {
                s(i, a, m[a].get, m[a].set)
            })
        }
    }
}, []);
require.define({
    "gamejs/utils/vectors": function (y, i) {
        var a = y("./math");
        i.distance = function (a, b) {
            return r(s(a, b))
        };
        var s = i.substract = function (a, b) {
            return [a[0] - b[0], a[1] - b[1]]
        };
        i.add = function (a, b) {
            return [a[0] + b[0], a[1] + b[1]]
        };
        i.multiply = function (a, b) {
            if (typeof b === "number") return [a[0] * b, a[1] * b];
            return [a[0] * b[0], a[1] * b[1]]
        };
        i.divide = function (a, b) {
            if (typeof b === "number") return [a[0] / b, a[1] / b];
            throw Error("only divide by scalar supported");
        };
        var r = i.len = function (a) {
            return Math.sqrt(a[0] * a[0] + a[1] * a[1])
        };
        i.unit = function (a) {
            var b = r(a);
            if (b) return [a[0] / b, a[1] / b];
            return [0, 0]
        };
        i.rotate = function (i, b) {
            b = a.normaliseRadians(b);
            return [i[0] * Math.cos(b) - i[1] * Math.sin(b), i[0] * Math.sin(b) + i[1] * Math.cos(b)]
        };
        i.dot = function (a, b) {
            return a[0] * b[0] + a[1] * b[1]
        };
        i.angle = function (a, b) {
            var c = r(a),
                g = r(b);
            return c && g ? Math.acos((a[0] * b[0] + a[1] * b[1]) / (c * g)) : 0
        }
    }
}, ["gamejs/utils/math"]);
require.define({
    gamejs: function (y, i) {
        function a() {
            var a = 0,
                b = 0,
                c = 0,
                f = 0;
            if (arguments.length === 2) arguments[0] instanceof Array && arguments[1] instanceof Array ? (a = arguments[0][0], b = arguments[0][1], c = arguments[1][0], f = arguments[1][1]) : (a = arguments[0], b = arguments[1]);
            else if (arguments.length === 1 && arguments[0] instanceof Array) a = arguments[0][0], b = arguments[0][1], c = arguments[0][2], f = arguments[0][3];
            else if (arguments.length === 1 && arguments[0] instanceof g) a = arguments[0].left, b = arguments[0].top, c = arguments[0].width,
            f = arguments[0].height;
            else if (arguments.length === 4) a = arguments[0], b = arguments[1], c = arguments[2], f = arguments[3];
            else throw Error("not a valid rectangle specification");
            return {
                left: a || 0,
                top: b || 0,
                width: c || 0,
                height: f || 0
            }
        }
        var s = y("./gamejs/utils/matrix"),
            r = y("./gamejs/utils/objects"),
            m = ["info", "warn", "error", "fatal"],
            b = 2;
        i.setLogLevel = function (a) {
            if (typeof a === "string" && m.indexOf(a)) b = m.indexOf(a);
            else if (typeof a === "number") b = a;
            else throw Error("invalid logLevel ", a, " Must be one of: ", m);
            return b
        };
        var c = i.log = function () {
            var a = Array.prototype.slice.apply(arguments, [0]);
            a.unshift(Date.now());
            window.console !== void 0 && console.log.apply && console.log.apply(console, a)
        };
        i.info = function () {
            b <= m.indexOf("info") && c.apply(this, arguments)
        };
        i.warn = function () {
            b <= m.indexOf("warn") && c.apply(this, arguments)
        };
        i.error = function () {
            b <= m.indexOf("error") && c.apply(this, arguments)
        };
        i.fatal = function () {
            b <= m.indexOf("fatal") && c.apply(this, arguments)
        };
        var g = i.Rect = function () {
            var b = a.apply(this, arguments);
            this.left = b.left;
            this.top = b.top;
            this.width = b.width;
            this.height = b.height;
            return this
        };
        r.accessors(g.prototype, {
            bottom: {
                get: function () {
                    return this.top + this.height
                },
                set: function (a) {
                    this.top = a - this.height
                }
            },
            right: {
                get: function () {
                    return this.left + this.width
                },
                set: function (a) {
                    this.left = a - this.width
                }
            },
            center: {
                get: function () {
                    return [this.left + this.width / 2, this.top + this.height / 2]
                },
                set: function () {
                    var b = a.apply(this, arguments);
                    this.left = b.left - this.width / 2;
                    this.top = b.top - this.height / 2
                }
            }
        });
        g.prototype.move = function () {
            var b = a.apply(this,
            arguments);
            return new g(this.left + b.left, this.top + b.top, this.width, this.height)
        };
        g.prototype.moveIp = function () {
            var b = a.apply(this, arguments);
            this.left += b.left;
            this.top += b.top
        };
        g.prototype.clip = function (a) {
            if (!this.collideRect(a)) return new g(0, 0, 0, 0);
            var b, c, f, i;
            if (this.left >= a.left && this.left < a.right) b = this.left;
            else if (a.left >= this.left && a.left < this.right) b = a.left;
            this.right > a.left && this.right <= a.right ? f = this.right - b : a.right > this.left && a.right <= this.right && (f = a.right - b);
            if (this.top >= a.top && this.top < a.bottom) c = this.top;
            else if (a.top >= this.top && a.top < this.bottom) c = a.top;
            this.bottom > a.top && this.bottom <= a.bottom ? i = this.bottom - c : a.bottom > this.top && a.bottom <= this.bottom && (i = a.bottom - c);
            return new g(b, c, f, i)
        };
        g.prototype.union = function (a) {
            return new g(Math.min(this.left, a.left), Math.min(this.top, a.top), Math.max(this.right, a.right), Math.max(this.bottom, a.bottom))
        };
        g.prototype.collidePoint = function () {
            var b = a.apply(this, arguments);
            return this.left <= b.left && b.left <= this.right && this.top <= b.top && b.top <= this.bottom
        };
        g.prototype.collideRect = function (a) {
            return !(this.left > a.right || this.right < a.left || this.top > a.bottom || this.bottom < a.top)
        };
        g.prototype.collideLine = function (a, b) {
            var c = a[0],
                f = a[1],
                g = b[0],
                i = b[1],
                l = !0,
                m = !0,
                p = !0;
            [
                [this.left, this.top],
                [this.left, this.bottom],
                [this.right, this.top],
                [this.right, this.bottom]
            ].map(function (a) {
                return (i - f) * a[0] + (c - g) * a[1] + (g * f - c * i)
            }).forEach(function (a) {
                a > 0 ? m = !1 : a < 0 ? l = !1 : a === 0 && (p = !1)
            }, this);
            if ((l || m) && p) return !1;
            return !(c > this.right && g > this.right || c < this.left && g < this.left || f < this.top && i < this.top || f > this.bottom && i > this.bottom)
        };
        g.prototype.toString = function () {
            return ["[", this.left, ",", this.top, "] [", this.width, ",", this.height, "]"].join("")
        };
        g.prototype.clone = function () {
            return new g(this)
        };
        var p = i.Surface = function () {
            var b = a.apply(this, arguments),
                c = b.left,
                f = b.top;
            if (arguments.length == 1 && arguments[0] instanceof g) c = b.width, f = b.height;
            this._matrix = s.identity();
            this._canvas = document.createElement("canvas");
            this._canvas.width = c;
            this._canvas.height = f;
            this._blitAlpha = 1;
            return this
        };
        p.prototype.blit = function (a, b, c) {
            if (b instanceof g) {
                var b = b.clone(),
                    f = a.getSize();
                if (!b.width) b.width = f[0];
                if (!b.height) b.height = f[1]
            } else b = b && b instanceof Array && b.length == 2 ? new g(b, a.getSize()) : new g([0, 0], a.getSize());
            c = c instanceof g ? c : c && c instanceof Array && c.length == 2 ? new g(c, a.getSize()) : new g([0, 0], a.getSize());
            if (isNaN(b.left) || isNaN(b.top) || isNaN(b.width) || isNaN(b.height)) throw Error("[blit] bad parameters, destination is " + b);
            this.context.save();
            f = s.translate(s.identity(),
            b.left, b.top);
            f = s.multiply(f, a._matrix);
            this.context.transform(f[0], f[1], f[2], f[3], f[4], f[5]);
            srcRect = a.getRect();
            this.context.globalAlpha = a._blitAlpha;
            this.context.drawImage(a.canvas, c.left, c.top, c.width, c.height, 0, 0, b.width, b.height);
            this.context.restore();
            return this.rect.clip(b)
        };
        p.prototype.getSize = function () {
            return [this.canvas.width, this.canvas.height]
        };
        p.prototype.getRect = function () {
            return new g([0, 0], this.getSize())
        };
        p.prototype.fill = function (a) {
            this.context.save();
            this.context.fillStyle = a || "#000000";
            this.context.fillRect(0, 0, this.canvas.width, this.canvas.height);
            this.context.restore()
        };
        p.prototype.clear = function () {
            var a = this.getSize();
            this.context.clearRect(0, 0, a[0], a[1])
        };
        r.accessors(p.prototype, {
            rect: {
                get: function () {
                    return this.getRect()
                }
            },
            context: {
                get: function () {
                    return this._canvas.getContext("2d")
                }
            },
            canvas: {
                get: function () {
                    return this._canvas
                }
            }
        });
        p.prototype.clone = function () {
            var a = new p(this.getRect());
            a.blit(this);
            return a
        };
        p.prototype.getAlpha = function () {
            return 1 - this._blitAlpha
        };
        p.prototype.setAlpha = function (a) {
            if (!isNaN(a) && !(a < 0 || a > 1)) return this._blitAlpha = 1 - a, 1 - this._blitAlpha
        };
        p.prototype.getImageData = function () {
            var a = this.getSize();
            return this.context.getImageData(0, 0, a[0], a[1]).data
        };
        i.display = y("./gamejs/display");
        i.draw = y("./gamejs/draw");
        i.event = y("./gamejs/event");
        i.font = y("./gamejs/font");
        i.http = y("./gamejs/http");
        i.image = y("./gamejs/image");
        i.mask = y("./gamejs/mask");
        i.mixer = y("./gamejs/mixer");
        i.sprite = y("./gamejs/sprite");
        i.surfacearray = y("./gamejs/surfacearray");
        i.time = y("./gamejs/time");
        i.transform = y("./gamejs/transform");
        i.utils = {
            arrays: y("./gamejs/utils/arrays"),
            objects: y("./gamejs/utils/objects"),
            matrix: y("./gamejs/utils/matrix"),
            vectors: y("./gamejs/utils/vectors"),
            math: y("./gamejs/utils/math")
        };
        i.pathfinding = {
            astar: y("./gamejs/pathfinding/astar")
        };
        var f = i,
            l = {};
        i.ready = function (a) {
            function b() {
                if (!document.body) return window.setTimeout(b, 50);
                i = f.image.preload(l);
                try {
                    g = f.mixer.preload(l)
                } catch (a) {
                    f.debug("Error loading audio files ", a)
                }
                window.setTimeout(c,
                50)
            }
            function c() {
                if (i() < 1 || g() < 1) return window.setTimeout(c, 100);
                f.display.init();
                f.image.init();
                f.mixer.init();
                f.event.init();
                a()
            }
            var g = null,
                i = null;
            f.time.init();
            window.setTimeout(b, 13);
            return function () {
                if (i) return 0.5 * i() + 0.5 * g();
                return 0.1
            }
        };
        i.preload = function (a) {
            a.forEach(function (a) {
                l[a] = ((window.$g && $g.resourceBaseHref || ".") + "/" + a).replace(/\/+/g, "/")
            }, this)
        }
    }
}, ["gamejs/utils/matrix", "gamejs/utils/objects", "gamejs/display", "gamejs/draw", "gamejs/event", "gamejs/font", "gamejs/http", "gamejs/image",
    "gamejs/mask", "gamejs/mixer", "gamejs/sprite", "gamejs/surfacearray", "gamejs/time", "gamejs/transform", "gamejs/utils/arrays", "gamejs/utils/vectors", "gamejs/utils/math", "gamejs/pathfinding/astar"]);
require.define({
    main: function (y) {
        function i(a) {
            this.position = [a.x, a.y];
            this.car = a.car;
            this.revolving = a.revolving;
            this.powered = a.powered;
            var b = new r.b2BodyDef;
            b.type = r.b2Body.b2_dynamicBody;
            b.position = this.car.body.GetWorldPoint(new r.b2Vec2(this.position[0], this.position[1]));
            b.angle = this.car.body.GetAngle();
            this.body = l.CreateBody(b);
            b = new r.b2FixtureDef;
            b.density = 1;
            b.isSensor = !0;
            b.shape = new r.b2PolygonShape;
            b.shape.SetAsBox(a.width / 2, a.length / 2);
            this.body.CreateFixture(b);
            this.revolving ? (a = new r.b2RevoluteJointDef,
            a.Initialize(this.car.body, this.body, this.body.GetWorldCenter()), a.enableMotor = !1) : (a = new r.b2PrismaticJointDef, a.Initialize(this.car.body, this.body, this.body.GetWorldCenter(), new r.b2Vec2(1, 0)), a.enableLimit = !0, a.lowerTranslation = a.upperTranslation = 0);
            l.CreateJoint(a)
        }
        function a(a) {
            this.steer = c;
            this.accelerate = g;
            this.max_steer_angle = a.max_steer_angle;
            this.max_speed = a.max_speed;
            this.power = a.power;
            this.wheel_angle = 0;
            var d = new r.b2BodyDef;
            d.type = r.b2Body.b2_dynamicBody;
            d.position = new r.b2Vec2(a.position[0],
            a.position[1]);
            d.angle = b.radians(a.angle);
            d.linearDamping = 0.15;
            d.bullet = !0;
            d.angularDamping = 0.3;
            this.body = l.CreateBody(d);
            d = new r.b2FixtureDef;
            d.density = 1;
            d.friction = 0.3;
            d.restitution = 0.4;
            d.shape = new r.b2PolygonShape;
            d.shape.SetAsBox(a.width / 2, a.length / 2);
            this.body.CreateFixture(d);
            this.wheels = [];
            var f;
            for (f = 0; f < a.wheels.length; f++) d = a.wheels[f], d.car = this, this.wheels.push(new i(d))
        }
        var s = y("gamejs"),
            r = y("./Box2dWeb-2.1.a.3"),
            m = y("gamejs/utils/vectors"),
            b = y("gamejs/utils/math"),
            c = 0,
            g = 0,
            p = 400 / 15,
            f = {}, l, z = new s.font.Font("16px Sans-serif"),
            d = {
                accelerate: s.event.K_UP,
                brake: s.event.K_DOWN,
                steer_left: s.event.K_LEFT,
                steer_right: s.event.K_RIGHT
            }, t = function (a) {
                this.size = a.size;
                var b = new r.b2BodyDef;
                b.position = new r.b2Vec2(a.position[0], a.position[1]);
                b.angle = 0;
                b.fixedRotation = !0;
                this.body = l.CreateBody(b);
                a = new r.b2FixtureDef;
                a.shape = new r.b2PolygonShape;
                a.shape.SetAsBox(this.size[0] / 2, this.size[1] / 2);
                a.restitution = 0.4;
                this.body.CreateFixture(a);
                return this
            };
        i.prototype.setAngle = function (a) {
            this.body.SetAngle(this.car.body.GetAngle() + b.radians(a))
        };
        i.prototype.getLocalVelocity = function () {
            var a = this.car.body.GetLocalVector(this.car.body.GetLinearVelocityFromLocalPoint(new r.b2Vec2(this.position[0], this.position[1])));
            return [a.x, a.y]
        };
        i.prototype.getDirectionVector = function () {
            return m.rotate(this.getLocalVelocity()[1] > 0 ? [0, 1] : [0, -1], this.body.GetAngle())
        };
        i.prototype.getKillVelocityVector = function () {
            var a = this.body.GetLinearVelocity(),
                b = this.getDirectionVector(),
                a = m.dot([a.x, a.y], b);
            return [b[0] * a, b[1] * a]
        };
        i.prototype.killSidewaysVelocity = function () {
            var a = this.getKillVelocityVector();
            this.body.SetLinearVelocity(new r.b2Vec2(a[0], a[1]))
        };
        a.prototype.getPoweredWheels = function () {
            for (var a = [], b = 0; b < this.wheels.length; b++) this.wheels[b].powered && a.push(this.wheels[b]);
            return a
        };
        a.prototype.getLocalVelocity = function () {
            var a = this.body.GetLocalVector(this.body.GetLinearVelocityFromLocalPoint(new r.b2Vec2(0, 0)));
            return [a.x, a.y]
        };
        a.prototype.getRevolvingWheels = function () {
            for (var a = [], b = 0; b < this.wheels.length; b++) this.wheels[b].revolving && a.push(this.wheels[b]);
            return a
        };
        a.prototype.getSpeedKMH = function () {
            var a = this.body.GetLinearVelocity();
            return m.len([a.x, a.y]) / 1E3 * 3600
        };
        a.prototype.setSpeed = function (a) {
            var b = this.body.GetLinearVelocity(),
                b = m.unit([b.x, b.y]),
                b = new r.b2Vec2(b[0] * (a * 1E3 / 3600), b[1] * (a * 1E3 / 3600));
            this.body.SetLinearVelocity(b)
        };
        a.prototype.update = function (a) {
            var b;
            for (b = 0; b < this.wheels.length; b++) this.wheels[b].killSidewaysVelocity();
            b = this.max_steer_angle / 200 * a;
            this.wheel_angle = this.steer == 1 ? Math.min(Math.max(this.wheel_angle, 0) + b, this.max_steer_angle) : this.steer == 2 ? Math.max(Math.min(this.wheel_angle, 0) - b, -this.max_steer_angle) : 0;
            a = this.getRevolvingWheels();
            for (b = 0; b < a.length; b++) a[b].setAngle(this.wheel_angle);
            b = this.accelerate == 1 && this.getSpeedKMH() < this.max_speed ? [0, -1] : this.accelerate == 2 ? this.getLocalVelocity()[1] < 0 ? [0, 1.3] : [0, 0.7] : [0, 0];
            var c = [this.power * b[0], this.power * b[1]],
                a = this.getPoweredWheels();
            for (b = 0; b < a.length; b++) {
                var d = a[b].body.GetWorldCenter();
                a[b].body.ApplyForce(a[b].body.GetWorldVector(new r.b2Vec2(c[0], c[1])), d)
            }
            this.getSpeedKMH() < 4 && this.accelerate == g && this.setSpeed(0)
        };
        s.ready(function () {
            var b = s.display.setMode([600, 400]);
            l = new r.b2World(new r.b2Vec2(0, 0), !1);
            var i = new r.b2DebugDraw;
            i.SetSprite(b._canvas.getContext("2d"));
            i.SetDrawScale(15);
            i.SetFillAlpha(0.5);
            i.SetLineThickness(1);
            i.SetFlags(r.b2DebugDraw.e_shapeBit);
            l.SetDebugDraw(i);
            var m = new a({
                width: 2,
                length: 4,
                position: [10, 10],
                angle: 180,
                power: 60,
                max_steer_angle: 20,
                max_speed: 60,
                wheels: [{
                    x: -1,
                    y: -1.2,
                    width: 0.4,
                    length: 0.8,
                    revolving: !0,
                    powered: !0
                }, {
                    x: 1,
                    y: -1.2,
                    width: 0.4,
                    length: 0.8,
                    revolving: !0,
                    powered: !0
                }, {
                    x: -1,
                    y: 1.2,
                    width: 0.4,
                    length: 0.8,
                    revolving: !1,
                    powered: !1
                }, {
                    x: 1,
                    y: 1.2,
                    width: 0.4,
                    length: 0.8,
                    revolving: !1,
                    powered: !1
                }]
            }),
                i = [];
            i.push(new t({
                size: [40, 1],
                position: [20, 0.5]
            }));
            i.push(new t({
                size: [1, p - 2],
                position: [0.5, p / 2]
            }));
            i.push(new t({
                size: [40, 1],
                position: [20, p - 0.5]
            }));
            i.push(new t({
                size: [1, p - 2],
                position: [39.5, p / 2]
            }));
            var y = [20, p / 2];
            i.push(new t({
                size: [1, 6],
                position: [y[0] - 3, y[1]]
            }));
            i.push(new t({
                size: [1, 6],
                position: [y[0] + 3, y[1]]
            }));
            i.push(new t({
                size: [5, 1],
                position: [y[0], y[1] + 2.5]
            }));
            s.time.fpsCallback(function (a) {
                s.event.get().forEach(function (a) {
                    a.type === s.event.KEY_DOWN ? f[a.key] = !0 : a.type === s.event.KEY_UP && (f[a.key] = !1)
                });
                m.accelerate = f[d.accelerate] ? 1 : f[d.brake] ? 2 : g;
                m.steer = f[d.steer_right] ? 1 : f[d.steer_left] ? 2 : c;
                m.update(a);
                l.Step(a / 1E3, 10, 8);
                l.ClearForces();
                s.draw.rect(b, "#FFFFFF", new s.Rect([0, 0], [600, 400]), 0);
                l.DrawDebugData();
                b.blit(z.render("FPS: " + parseInt(1E3 / a)), [25, 25]);
                b.blit(z.render("SPEED: " + parseInt(Math.ceil(m.getSpeedKMH())) + " km/h"), [25, 55])
            }, this,
            60)
        })
    }
}, ["gamejs", "Box2dWeb-2.1.a.3", "gamejs/utils/vectors", "gamejs/utils/math"]);

//require.setModuleRoot('./javascript/');
require.run('main');