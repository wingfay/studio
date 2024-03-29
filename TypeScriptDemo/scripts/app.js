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
class Person {
    //构造函数
    constructor(name, age) {
        this.name = name;
        this.age = age;
    }
    //实例方法
    sayHi() {
        return "hi,我的姓名是:" + this.name + ",我年龄是:" + this.age;
    }
    //静态方法
    static hello() {
        console.log("hello!!");
    }
    //静态方法中可以返回静态属性，，静态成员只能使用类名.静态成员的方式进行访问。
    static area(r) {
        return Person.PI * r * r;
    }
}
//静态属性
Person.PI = Math.PI;
//计算r为3的圆的面积 ，调用静态方法不需要new实例对象，直接用类名.静态方法调用即可。
console.log(Person.area(3)); //28.274333882308138
//new 对象
var p = new Person("张三", 22);
//调用对象的(实例)方法
console.log(p.sayHi());
class Student extends Person {
    constructor(name, age, score) {
        //子类调用父类构造函数进行初始化
        super(name, age);
        this.score = score;
    }
    //子类重写(覆盖)父类中的方法
    sayHi() {
        return "hi,我的姓名是:" + this.name + ",我年龄是:" + this.age + ",我的成绩是:" + this.score;
    }
    //子类中扩展的方法
    study() {
        return this.name + "在学习";
    }
}
var stu = new Student("李四", 24, 90);
console.log(stu.sayHi());
console.log(stu.study());
//# sourceMappingURL=app.js.map