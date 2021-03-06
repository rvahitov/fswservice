﻿<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <section name="FileWatcherService" type="FileWatcherService.Configuration.FileWatcherServiceSection, FileWatcherService.Configuration"/>
  </configSections>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.5.2" />
    </startup>
  
  <FileWatcherService xmlns="uri:configuration.file-watcher-service">
    <!--Provide name of windows service that have to be stopped on file system event-->
    <stopService name="TeamViewer 9"/>
    <!--Provide file name patterns-->

    <sendMail>
      <server host="smtp.yandex.ru" port="25" enableSSL="true" login="****" password="*******"/>
      <message from="user@user.ru" subjectTemplate="FileWatcherService - {event}" bodyTemplatePath="messageTemplate.html" isBodyHtml="true">
        <recipient address="user2@user.com"/>
        <recipient address="user3@user.com"/>
      </message>
    </sendMail>
    
    <patterns>
      <pattern value="*.txt"/>
      <pattern value="ab*.tst"/>
    </patterns>
    <watch>
      <!--Define directories to watch-->
      <!--Path  - absolute path of directory-->
      <!--Event - type of file system event to listen to. Illegal values is created, changed, deleted, renamed or theirs combinations with "|"-->
      <!--Include sub directories for watching-->
      <directory path="e:\temp" event="created | renamed" includeSubDirs="true"/>
    </watch>
  </FileWatcherService>
  
  
</configuration>
