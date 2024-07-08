using System;
using System.IO;
using System.Xml;
using testAssistant.util;


public class XmlFile
{
    private readonly XmlDocument doc;
    private readonly string file;
    public delegate void ModifyFile();

    public XmlFile(string filePath) {
        if (!FileUtil.checkFile(filePath)) {
            throw new Exception();
        }

        try {
            doc = new XmlDocument();
            doc.Load(filePath);
            file = filePath;
        }
        catch (Exception e) {
            throw new Exception(string.Format("xmlfile {0} 文件加载错误", Path.GetFileName(filePath)), e);
        }
    }

    public XmlNode findNodeByName(string name) {
        return XmlUtil.selectSingleNodeByName(doc, name);
    }

    public XmlNode findNodeByPattern(string xpath) {
        return XmlUtil.selectSingleNodeByPattern(doc, xpath);
    }

    public void copyNode(string xpath, XmlNode sourceNode) {
        if (sourceNode == null || sourceNode.NodeType == XmlNodeType.Comment) {
            return;
        }
        copyNode(xpath, sourceNode, sourceNode.Name, true,null);
    }
    public void copyNode(string xpath, XmlNode sourceNode, string name,bool copyAttribute, XmlUtil.modifyContent contentCondition) {
        XmlUtil.copyNode(doc, findNodeByPattern(xpath), sourceNode, name,copyAttribute, contentCondition, null);
    }
    public void copyNode(string xpath, XmlNode sourceNode, XmlUtil.modifyContent contentCondition) {
        XmlUtil.copyNode(doc, findNodeByPattern(xpath), sourceNode, sourceNode.LocalName, contentCondition);
    }
    public void deleteNode(string matcher) {
        if (string.IsNullOrEmpty(matcher)) {
            return;
        }

        XmlNode tmp;
        if (matcher.Contains("/")) {
            tmp = findNodeByPattern(matcher);
        }
        else {
            tmp = findNodeByName(matcher);
        }
        XmlUtil.delNode(tmp);
    }

    public void updateNode(string xpath, XmlUtil.modifyContent contentCondition) {
        XmlUtil.updateNode(doc, findNodeByPattern(xpath), contentCondition);
    }
    public void updateAndSave(ModifyFile modify) {
        modify();
        this.save();
    }

    public void save() {
        doc.Save(file);
    }
}