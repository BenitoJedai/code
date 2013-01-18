link source from https://github.com/PrimaryFeather/Starling-Framework.git

  <ItemGroup>
    <Content Include="X:\opensource\github\Starling-Framework\starling\src\**\*.*">
      <Link>opensource\github.com\Starling-Framework\src\%(RecursiveDir)%(FileName)%(Extension)</Link>
    </Content>
  </ItemGroup>