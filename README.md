# Database Segmentation

Para realizar a segmentação dos dados em imagens MALIGNAS e BENIGNAS, é necessário executar o código desenvolvido.

Dasdo que o arquivo do banco de dados é extenso demais para ser versionado, é necessário baixá-lo separadamente e posicioná-lo no diretório `database-segmentation/DataSegmentation`. O arquivo deve ser posicionado nesse diretório COMPACTADO, ou seja, do jeito que foi instalado.

O código por sua vez, efetuará a separação das imagens em 3 diretórios distintos (mas somente 2 são usados efetivamente), os quais serão criados dentro de uma pasta especificada pela variável `destinationOfSegmentedImages` em `database-segmentation/DataSegmentation/DataSegmentation/Program.cs`. É fundamental ajustar essa variável para refletir o diretório desejado para o armazenamento das imagens segmentadas. Este procedimento visa otimizar o gerenciamento de grandes volumes de dados, permitindo a execução eficiente da segmentação sem a necessidade de incluir o arquivo de banco de dados no repositório de controle de versão.

---

## Hierarquia dos arquivos

O conteúdo da pasta descompactada derivada de `achive.zip` é dividido da seguinte maneira:

1. CSV

A pasta csv contém 4 arquivos principais em formato CSV:
- **calc_case_description_test_set**
- **calc_case_description_train_set**
- **mass_case_description_test_set**
- **mass_case_description_train_set**

Os arquivos que começam com **calc_** são imagens de calcificação. Em contrapartida, os arquivos que começam com **mass_** são imagens de massa. São usados somente imagens de massa.

Dentro de cada arquivo existem as seguinte colunas, das quais somente as colunas em negrito são usadas:
- **patient_id** - id do paciente
- breast_density
- left or right breast
- image view
- abnormality id
- abnormality type
- mass shape
- mass margins
- assessment
- **pathology**	
- subtlety
- image file path (imagens completas)
- **cropped image file path** (imagens cortadas, com foco na massa)
- ROI mask file path (imagens de região de interesse)


A coluna `cropped image file path` indica o caminho da imagem, dentro da pasta `jpeg`.

2. JPEG

A pasta `jpeg` abriga diversas subpastas, definidas nos campos de path dos arquivos CSV. Cada subpasta contém as imagens indicadas. 