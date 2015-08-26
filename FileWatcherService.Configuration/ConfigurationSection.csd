<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="d0ed9acb-0435-4532-afdd-b5115bc4d562" namespace="FileWatcherService.Configuration" xmlSchemaNamespace="uri:configuration.file-watcher-service" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
  <typeDefinitions>
    <externalType name="String" namespace="System" />
    <externalType name="Boolean" namespace="System" />
    <externalType name="Int32" namespace="System" />
    <externalType name="Int64" namespace="System" />
    <externalType name="Single" namespace="System" />
    <externalType name="Double" namespace="System" />
    <externalType name="DateTime" namespace="System" />
    <externalType name="TimeSpan" namespace="System" />
    <enumeratedType name="ListenerType" namespace="FileWatcherService.Configuration" isFlags="true">
      <literals>
        <enumerationLiteral name="Created" value="1" />
        <enumerationLiteral name="Deleted" value="4" />
        <enumerationLiteral name="Changed" value="2" />
        <enumerationLiteral name="Renamed" value="8" />
      </literals>
    </enumeratedType>
  </typeDefinitions>
  <configurationElements>
    <configurationSection name="FileWatcherServiceSection" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="FileWatcherService">
      <elementProperties>
        <elementProperty name="Patterns" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="patterns" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/FilePatterns" />
          </type>
        </elementProperty>
        <elementProperty name="StopService" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="stopService" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/StopService" />
          </type>
        </elementProperty>
        <elementProperty name="SendMail" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="sendMail" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/SendMail" />
          </type>
        </elementProperty>
        <elementProperty name="Watch" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="watch" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/WatchCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElementCollection name="FilePatterns" xmlItemName="pattern" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/FilePattern" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="FilePattern">
      <attributeProperties>
        <attributeProperty name="Value" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="value" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Regex" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="regex" isReadOnly="false" defaultValue="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Boolean" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElement name="StopService">
      <attributeProperties>
        <attributeProperty name="Name" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="name" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElement name="Directory">
      <attributeProperties>
        <attributeProperty name="Path" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="path" isReadOnly="false" defaultValue="&quot;someFile&quot;">
          <validator>
            <callbackValidatorMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/FilePathValidator" />
          </validator>
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Event" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="event" isReadOnly="false" defaultValue="&quot;created&quot;">
          <validator>
            <callbackValidatorMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/WatchEvent" />
          </validator>
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="IncludeSubDirs" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="includeSubDirs" isReadOnly="false" defaultValue="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Boolean" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElementCollection name="WatchCollection" xmlItemName="directory" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Directory" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="MailReceiverAddress">
      <attributeProperties>
        <attributeProperty name="Address" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="address" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElement name="SmtpServer">
      <attributeProperties>
        <attributeProperty name="Host" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="host" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Port" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="port" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="EnableSSL" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="enableSSL" isReadOnly="false" defaultValue="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="Login" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="login" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Password" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="password" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElementCollection name="Message" xmlItemName="recipient" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <attributeProperties>
        <attributeProperty name="SubjectTemplate" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="subjectTemplate" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="BodyTemplatePath" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="bodyTemplatePath" isReadOnly="false" defaultValue="&quot;body.txt&quot;">
          <validator>
            <callbackValidatorMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/FilePathValidator" />
          </validator>
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="IsBodyHtml" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="isBodyHtml" isReadOnly="false" defaultValue="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="From" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="from" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <itemType>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/MailReceiverAddress" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="SendMail">
      <elementProperties>
        <elementProperty name="Server" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="server" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/SmtpServer" />
          </type>
        </elementProperty>
        <elementProperty name="Message" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="message" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Message" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationElement>
  </configurationElements>
  <propertyValidators>
    <validators>
      <callbackValidator name="FilePathValidator" callback="Validate" />
      <callbackValidator name="WatchEvent" callback="Validate" />
    </validators>
  </propertyValidators>
</configurationSectionModel>