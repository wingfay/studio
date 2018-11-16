function sayHello() {
    const compiler = (document.getElementById("compiler") as HTMLInputElement).value;
    const framework = (document.getElementById("framework") as HTMLInputElement).value;
    return `Hello from ${compiler} and ${framework}!`;
}

function choose1<T>(a: T, b: T): T { return Math.random() > 0.5 ? a : b }
var b = choose1<string | number>('hello', 42); // OK


const halfPi = Math.PI / 2;

var foo: boolean;

if (foo) {

    let x = 'hello';
    console.log(x); // Error, cannot refer to x before its declaration
} else {
    console.log("ss"); // Error, x is not declared in this block
}

let x: number;
let y: number | null;
let z: number | undefined;
x;  // 错误，使用前未赋值
y;  // 错误，使用前未赋值
z;  // 正确
x = 1;
y = null;
x;  // 正确
y;  // 正确