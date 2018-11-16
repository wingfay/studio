function sayHello() {
    const compiler = document.getElementById("compiler").value;
    const framework = document.getElementById("framework").value;
    return `Hello from ${compiler} and ${framework}!`;
}
function choose1(a, b) { return Math.random() > 0.5 ? a : b; }
var b = choose1('hello', 42); // OK
const halfPi = Math.PI / 2;
var foo;
if (foo) {
    let x = 'hello';
    console.log(x); // Error, cannot refer to x before its declaration
}
else {
    console.log("ss"); // Error, x is not declared in this block
}
let x;
let y;
let z;
x; // 错误，使用前未赋值
y; // 错误，使用前未赋值
z; // 正确
x = 1;
y = null;
x; // 正确
y; // 正确
//# sourceMappingURL=app.js.map