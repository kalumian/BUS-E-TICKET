import {
    CCard,
    CCardBody,
    CCol,
    CContainer,
    CRow,
    CCardHeader,
  } from '@coreui/react';
  // ... existing imports and initial code ...

  const About = () => {
    const technologies = {
      frontend: [
        { name: 'React', color: 'primary' },
        { name: 'TypeScript', color: 'secondary' },
        { name: 'Next.js', color: 'dark' },
        { name: 'Material UI', color: 'info' },
        { name: 'Bootstrap', color: 'teal' },
        { name: 'SASS', color: 'pink' },
        { name: 'REST API Integration', color: 'warning' },
        { name: 'Redux', color: 'indigo' },
        { name: 'Axios', color: 'teal' },
        { name: 'Javascript', color: 'danger' },
        { name: 'DART', color: 'success' },
        { name: 'Windows Form', color: 'primary' },
        { name: 'Cookies', color: 'dark' },

      ],
      backend: [
        { name: '.NET Core', color: 'success' },
        { name: 'C#', color: 'teal' },
        { name: 'Node.js', color: 'success' },
        { name: 'Express.js', color: 'dark' },
        { name: 'Entity Framework', color: 'info' },
        { name: 'Identity Framework', color: 'primary' },
        { name: 'ADO.NET', color: 'danger' },
        { name: 'PostgreSQL', color: 'info' },
        { name: 'MongoDB', color: 'success' },
        { name: 'Firebase', color: 'warning' },
        { name: 'Token', color: 'teal' },
        { name: 'JWT', color: 'info' },
        { name: 'Authorization - Authentication', color: 'primary' },

      ],
      architecture: [
        { name: 'SOLID Principles', color: 'dark' },
        { name: 'N-Tier Architecture', color: 'secondary' },
        { name: 'Clean Code', color: 'info' },
        { name: 'Repository Pattern', color: 'success' },
        { name: 'Unit of Work', color: 'warning' },
        { name: 'IoC', color: 'danger' },
        { name: 'Dependency Injection', color: 'primary' },
        { name: 'REST APIs', color: 'info' },
      ]
    };

    const projects = [
      {
        name: 'BUS E-TICKET Platform',
        description: 'Graduation project: A scalable B2B2C SaaS system for intercity transportation',
        tech: 'C#, .NET API, PostgreSQL, SOLID, Clean Code',
        status: 'MVP Completed'
      },
      {
        name: 'Sanabel Al-Madina Website',
        description: 'Official website for charity association with volunteer & job portals',
        tech: 'WordPress, Web Development, Web Design',
        link: 'https://sanabel-almadina.sa/'
      },
      {
        name: 'United Valeria Educational Platform',
        description: 'Educational and tourism services platform for Arab community',
        tech: 'Full-Stack Development, WordPress, Custom Design'
      }
    ];

    return (
      <CContainer className="mt-4 About">
        <CCard className="shadow-sm developer-card mb-4">
          <CCardHeader className="bg-primary text-white">
            <h4 className="mb-0">About Me</h4>
          </CCardHeader>
          <CCardBody className="p-4">
            <CRow className="align-items-start">
              <CCol md={4} className="text-center mb-4 mb-md-0">
                <div className="developer-avatar mb-3">
                  <i className="fas fa-user-circle" style={{ fontSize: '5rem', color: 'var(--cui-primary)' }}></i>
                </div>
                <h5>Muhammad Kalumian</h5>
                <div className="text-muted mb-2">Full-Stack Developer</div>
                <div className="contact-info mb-3">
                  <div><i className="fas fa-envelope me-2"></i>muhammadkalumian@gmail.com</div>
                  <div><i className="fas fa-phone me-2"></i>+90 506 264 6625</div>
                  <div><i className="fas fa-map-marker-alt me-2"></i>Turkey</div>
                </div>
                <div className="social-links">
                  <a href="https://github.com/kalumian" target="_blank" className="text-decoration-none me-2">
                    <i className="fab fa-github"></i>
                  </a>
                  <a href="https://linkedin.com/in/yourprofile" target="_blank" className="text-decoration-none me-2">
                    <i className="fab fa-linkedin"></i>
                  </a>
                </div>
              </CCol>
              <CCol md={8}>
                <div className="p-3 bg-light rounded">
                  <h5 className="mb-3">Professional Summary</h5>
                  <p>
                    Started my programming journey in 2020, currently pursuing Software Engineering at Bandırma Onyedi Eylül University. 
                    Experienced in full-stack development with a focus on web technologies and scalable applications. 
                    Successfully completed various freelance projects including e-commerce platforms, educational websites, and management systems.
                  </p>
                  <div className="mt-4">
                    {/* ...existing technology stack code... */}
                  </div>
                </div>
              </CCol>
            </CRow>
          </CCardBody>
        </CCard>

        {/* Featured Projects Section */}
        <CCard className="shadow-sm">
          <CCardHeader className="bg-light">
            <h4 className="mb-0">Featured Projects</h4>
          </CCardHeader>
          <CCardBody className="p-4">
            <CRow className="g-4">
              {projects.map((project, index) => (
                <CCol md={4} key={index}>
                  <div className="project-card h-100 bg-light rounded p-3">
                    <h5 className="mb-2">{project.name}</h5>
                    <p className="text-muted small mb-3">{project.description}</p>
                    <div className="small">
                      <strong>Technologies:</strong> {project.tech}
                    </div>
                    {project.link && (
                      <a href={project.link} target="_blank" className="btn btn-sm btn-primary mt-2">
                        View Project
                      </a>
                    )}
                  </div>
                </CCol>
              ))}
            </CRow>
          </CCardBody>
        </CCard>
                <CCol md={8}>
        <div className="p-3 bg-light rounded">
            <h5 className="mb-3">Professional Summary</h5>
            <p>
            Started my programming journey in 2020, currently pursuing Software Engineering at Bandırma Onyedi Eylül University. 
            Experienced in full-stack development with a focus on web technologies and scalable applications. 
            Successfully completed various freelance projects including e-commerce platforms, educational websites, and management systems.
            </p>
            <div className="mt-4">
            <div className="mb-4">
                <h6 className="mb-3">Frontend Technologies</h6>
                <div className="tech-stack">
                {technologies.frontend.map((tech, index) => (
                    <span key={index} className={`badge bg-${tech.color} me-2 mb-2`}>
                    {tech.name}
                    </span>
                ))}
                </div>
            </div>

            <div className="mb-4">
                <h6 className="mb-3">Backend Technologies</h6>
                <div className="tech-stack">
                {technologies.backend.map((tech, index) => (
                    <span key={index} className={`badge bg-${tech.color} me-2 mb-2`}>
                    {tech.name}
                    </span>
                ))}
                </div>
            </div>

            <div className="mb-4">
                <h6 className="mb-3">Architecture & Patterns</h6>
                <div className="tech-stack">
                {technologies.architecture.map((tech, index) => (
                    <span key={index} className={`badge bg-${tech.color} me-2 mb-2`}>
                    {tech.name}
                    </span>
                ))}
                </div>
            </div>
            </div>
        </div>
        </CCol>
      </CContainer>
    );
};

export default About;