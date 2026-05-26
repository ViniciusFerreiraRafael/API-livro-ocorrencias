# API - Livro de ocorrências

O projeto visa solucionar um problema cotidiano das coordenações pedagógicas e secretarias de educação: a gestão manual e fragmentada do histórico disciplinar dos estudantes. 
A substituição do tradicional "caderno de ocorrências" físico por uma API RESTful garante a integridade dos dados e agiliza a tomada de decisão pedagógica.  

O escopo escolhido é altamente coeso e reflete uma necessidade real do ambiente educacional, justificando as 5 entidades do sistema. A arquitetura se baseia no evento da Ocorrencia, que é o registro central criado por um Professor para um Aluno específico. Para garantir a padronização e facilitar a geração de métricas, cada registro é vinculado a um MotivoInfracao predefinido. A organização dos alunos em uma Turma permite mapear a incidência de ocorrências de forma coletiva. Essa modelagem garante relacionamentos claros e consistentes (1:N e N:M) necessários para um sistema robusto de gerenciamento escolar.
