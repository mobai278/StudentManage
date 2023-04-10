#pragma checksum "C:\Users\86138\Downloads\Compressed\ASP.NET学生管理系统(公众号：程序员小R)\StudentManage\StudentManage\Areas\StudentManage\Views\Role\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "63e0cdf020abb9a3c6f306694b92b4f3b20f9c5c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_StudentManage_Views_Role_Index), @"mvc.1.0.view", @"/Areas/StudentManage/Views/Role/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"63e0cdf020abb9a3c6f306694b92b4f3b20f9c5c", @"/Areas/StudentManage/Views/Role/Index.cshtml")]
    #nullable restore
    public class Areas_StudentManage_Views_Role_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 2 "C:\Users\86138\Downloads\Compressed\ASP.NET学生管理系统(公众号：程序员小R)\StudentManage\StudentManage\Areas\StudentManage\Views\Role\Index.cshtml"
  
    Layout = null;

#line default
#line hidden
#nullable disable
            WriteLiteral("<style>\n    .layui-tree-icon {\n        height: 14px !important;\n        width: 14px !important;\n    }\n</style>\n\n");
            WriteLiteral("<div class=\"layui-card layadmin-header\">\n    <div class=\"layui-breadcrumb\" lay-filter=\"breadcrumb\" style=\"visibility: visible;padding: 15px;\">\n        <a");
            BeginWriteAttribute("lay-href", " lay-href=\"", 300, "\"", 311, 0);
            EndWriteAttribute();
            WriteLiteral(">主页</a><span");
            BeginWriteAttribute("lay-separator", " lay-separator=\"", 324, "\"", 340, 0);
            EndWriteAttribute();
            WriteLiteral(">/</span>\n        <a><cite>系统管理</cite></a><span");
            BeginWriteAttribute("lay-separator", " lay-separator=\"", 388, "\"", 404, 0);
            EndWriteAttribute();
            WriteLiteral(">/</span>\n        <a><cite>角色管理</cite></a>\n    </div>\n</div>\n\n");
            WriteLiteral(@"<div class=""demoTable"" style=""padding-left: 15px;"">
    <div style=""display: inline-block;"">
        名称：
        <div class=""layui-inline"">
            <input class=""layui-input"" name=""name"" id=""name"" autocomplete=""off"" placeholder=""请输入名称"">
        </div>
        <button class=""layui-btn"" data-type=""reload"">搜索</button>
    </div>
    <div style=""float: right;padding-right: 10px;"">
        <button class=""layui-btn"" data-type=""add"">新增</button>
    </div>
</div>

");
            WriteLiteral(@"<table class=""layui-hide"" id=""tableList"" lay-filter=""List""></table>

<script type=""text/html"" id=""barDemo"">
    <font color=""#F3F0F0""><a class=""layui-btn layui-btn-sm"" lay-event=""edit"">编辑</a></font>
    <a class=""layui-btn layui-btn-warm layui-btn-sm"" lay-event=""role"">设置权限</a>
    <font color=""#F3F0F0""><a class=""layui-btn layui-btn-danger layui-btn-sm"" lay-event=""del"">删除</a></font>

</script>

");
            WriteLiteral(@"<script type=""text/template"" id=""add_box"">
    <div class=""layui-form layui-form-pane"" style=""padding:10px"">
        <input type=""text"" name=""id"" id=""box_id"" class=""layui-input"" style=""display:none"">
        <div class=""layui-form-item"">
            <label class=""layui-form-label"">角色名称</label>
            <div class=""layui-input-block"">
                <input type=""text"" name=""name"" id=""box_name"" lay-verify=""required"" autocomplete=""off"" class=""layui-input"">
            </div>
        </div>
        <div class=""layui-form-item"">
            <label class=""layui-form-label"">描述</label>
            <div class=""layui-input-block"">
                <input type=""text"" name=""description"" id=""box_description"" lay-verify=""required"" autocomplete=""off"" class=""layui-input"">
            </div>
        </div>
        <button class=""layui-btn"" style=""display:none;"" lay-submit lay-filter=""submitForm"" id=""submitForm"">提交</button>
    </div>
</script>

");
            WriteLiteral(@"<script type=""text/template"" id=""role_box"">
    <input type=""text"" name=""roleId"" id=""roleId"" class=""layui-input"" style=""display:none"">
    <div class=""layui-form layui-form-pane"" style=""padding:10px"">
        <div class=""layui-form-item"">
            <div id=""roleTree""></div>
        </div>
        <button class=""layui-btn"" style=""display:none;"" lay-submit lay-filter=""submitForm"" id=""submitRole"">提交</button>
    </div>

</script>

<script>
    layui.use(['table', 'form', 'tree'],
        function () {
            var $ = layui.$;
            var table = layui.table;
            var form = layui.form;
            var tree = layui.tree;

            function initIndex() {
                table.render({
                    id: 'List',
                    elem: '#tableList',
                    url: '/StudentManage/Role/List',
                    title: '用户数据表',
                    cols: [
                        [
                            { type: 'checkbox', fixed: 'left' },
                            { field");
            WriteLiteral(@": 'id', title: 'ID', width: 80, fixed: 'left', unresize: true, sort: true },
                            { field: 'name', title: '用户名', width: 350, align: 'center' },
                            { field: 'description', title: '描述', width: 1000, align: 'center' },
                            { fixed: 'right', title: '操作', toolbar: '#barDemo', width: 300, align: 'center' }
                        ]
                    ],
                    done: function () { // 隐藏id列
                        $(""[data-field='id']"").css('display', 'none');
                    },
                    page: true
                });
            }

            //初始化列表页面
            initIndex();

            //初始化表单页面
            function initForm(formObj, dataObj) {
                console.log(dataObj);
                if (!!dataObj) {
                    $(""#box_id"").val(dataObj.id);
                    $(""#box_name"").val(dataObj.name);
                    $(""#box_description"").val(dataObj.description);
                }
           ");
            WriteLiteral(@"     //表单提交
                form.on('submit(submitForm)',
                    function (data) {
                        var url = '/StudentManage/Role/Add';
                        if (!!data.field.id) {
                            url = '/StudentManage/Role/Edit';
                        }
                        $.ajax({
                            type: ""POST"",
                            url: url,
                            data: data.field,
                            dataType: ""json"",
                            beforeSend: function () {
                            },
                            success: function (data) {
                                if (data.msg) {
                                    layer.msg(data.message, { icon: 6, time: 1000, shade: 0.08 });
                                    layer.close(formObj);
                                    initIndex();
                                } else {
                                    layer.msg(data.message, { icon: 5, time: 1000, shade: 0.");
            WriteLiteral(@"08 });
                                }
                            },
                            complete: function () {
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                            }
                        });
                    });
                form.render();
                return false;
            }

            //页面事件
            var active = {
                reload: function () {
                    var name = $('#name');
                    //执行重载
                    table.reload('List',
                        {
                            page: {
                                curr: 1 //重新从第 1 页开始
                            },
                            where: {
                                Name: name.val()
                            }
                        },
                        'data');
                },
                add: function () {
                    var str = $('#add_box').html();
");
            WriteLiteral(@"                    var addForm = layer.open({
                        title: ""添加"",
                        type: 1,
                        area: ['600px', '550px'],
                        content: str,
                        resize: true,
                        scrollbar: false,
                        fixed: false,
                        btn: ['确定', '取消'],
                        yes: function (index, obj) { //保存按钮事件
                            $(""#submitForm"").click();
                        },
                        btn2: function (index, obj) { //窗口关闭按钮事件
                            layer.close(index);
                        },
                        cancel: function (index) { //窗口关闭按钮事件
                            layer.close(index);
                        }
                    });

                    initForm(addForm);
                }
            };

            //监听页面工具事件（搜索、新增、刷新等）
            $('.demoTable .layui-btn').on('click',
                function () {
                    var ty");
            WriteLiteral(@"pe = $(this).data('type');
                    active[type] ? active[type].call(this) : '';
                });

            //监听行工具事件（编辑、删除等）
            table.on('tool(List)',
                function (obj) {
                    var data = obj.data;
                    console.log(obj);
                    if (obj.event === 'del') {
                        layer.confirm('真的删除行么？',
                            function (index) {
                                $.ajax({
                                    type: ""POST"",
                                    url: '/StudentManage/Role/Delete',
                                    data: { id: data.id },
                                    dataType: ""json"",
                                    beforeSend: function () {
                                    },
                                    success: function (data) {
                                        if (data.msg) {
                                            layer.msg(data.message, { icon: 6, time: 1000, shade");
            WriteLiteral(@": 0.08 });
                                            layer.close(index);
                                            initIndex();
                                        } else {
                                            layer.msg(data.message, { icon: 5, time: 1000, shade: 0.08 });
                                        }
                                    },
                                    complete: function () {
                                    },
                                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                                    }
                                });
                            });
                    }
                    if (obj.event === 'edit') {
                        var editForm = layer.open({
                            title: ""编辑"",
                            type: 1,
                            area: ['600px', '550px'],
                            content: $('#add_box').html(),
                            resize: true,");
            WriteLiteral(@"
                            scrollbar: false,
                            fixed: false,
                            btn: ['确定', '取消'],
                            yes: function (index, obj) { //保存按钮事件
                                $(""#submitForm"").click();
                            },
                            btn2: function (index, obj) { //窗口关闭按钮事件
                                layer.close(index);
                            },
                            cancel: function (index) { //窗口关闭按钮事件
                                layer.close(index);
                            }
                        });

                        initForm(editForm, data);
                    }
                    if (obj.event === 'role') {
                        var str = $('#role_box').html();

                        var roleForm = layer.open({
                            title: ""菜单权限"",
                            type: 1,
                            area: ['600px', '550px'],
                            content: str");
            WriteLiteral(@",
                            resize: true,
                            scrollbar: false,
                            fixed: false,
                            btn: ['确定', '取消'],
                            yes: function (index, obj) { //保存按钮事件
                                $(""#submitRole"").click();
                            },
                            btn2: function (index, obj) { //窗口关闭按钮事件
                                layer.close(index);
                            },
                            cancel: function (index) { //窗口关闭按钮事件
                                layer.close(index);
                            },
                            success: function (layero, index) {
                                $.ajax({
                                    type: 'GET',
                                    url: '/StudentManage/Module/ListByRoleId',
                                    dataType: ""json"",
                                    data: { roleId: data.id },
                                    asy");
            WriteLiteral(@"nc: false,
                                    success: function (res) {
                                        if (res.msg) {
                                            //渲染
                                            var inst1 = tree.render({
                                                elem: '#roleTree',  //绑定元素
                                                showCheckbox: true,  //是否显示复选框
                                                id: 'moduleTree',
                                                isJump: true,//是否允许点击节点时弹出新窗口跳转
                                                data: res.data
                                            });
                                        } else {
                                            layer.msg(res.message, { icon: 5, time: 1000, shade: 0.08 });
                                        }
                                    }
                                });

                                //表单提交 保存权限
                                form.on('submit(su");
            WriteLiteral(@"bmitForm)',
                                    function (res) {
                                        function getCheckedId(jsonObj) {
                                            var id = """";
                                            $.each(jsonObj,
                                                function (index, item) {
                                                    if (id != """") {
                                                        id = id + "","" + item.id;
                                                    } else {
                                                        id = item.id;
                                                    }
                                                    var i = getCheckedId(item.children);
                                                    if (i != """") {
                                                        id = id + "","" + i;
                                                    }
                                                });
                         ");
            WriteLiteral(@"                   return id;
                                        }

                                        var checkedData = tree.getChecked('moduleTree');
                                        var ids = getCheckedId(checkedData);
                                        $.ajax({
                                            type: ""POST"",
                                            url: '/StudentManage/Role/SaveRoleModule',
                                            data: { roleId: $(""#roleId"").val(), moduleIds: ids },
                                            dataType: ""json"",
                                            beforeSend: function () {
                                            },
                                            success: function (res) {
                                                if (res.msg) {
                                                    layer.msg(res.message, { icon: 6, time: 1000, shade: 0.08 });
                                                    layer.close(rol");
            WriteLiteral(@"eForm);
                                                    initIndex();
                                                } else {
                                                    layer.msg(res.message, { icon: 5, time: 1000, shade: 0.08 });
                                                }
                                            },
                                            complete: function () {
                                            },
                                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                            }
                                        });
                                    });

                                $(""#roleId"").val(data.id);
                                form.render();
                            }
                        });
                    }
                });
        });
</script>
");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
