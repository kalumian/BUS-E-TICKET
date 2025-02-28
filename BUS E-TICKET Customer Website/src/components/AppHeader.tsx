import React, { useEffect, useRef } from 'react';
import { NavLink } from 'react-router-dom'; // للتنقل بين الصفحات
import { useSelector, useDispatch } from 'react-redux';
import { useTranslation } from 'react-i18next';
import {
  CContainer,
  CDropdown,
  CDropdownItem,
  CDropdownMenu,
  CDropdownToggle,
  CHeader,
  CHeaderNav,
  CHeaderToggler,
  CNavLink,
  CNavItem,
  useColorModes,
  CButton,
} from '@coreui/react-pro';
import CIcon from '@coreui/icons-react';
import {
  cilContrast,
  cilApplicationsSettings,
  cilMoon,
  cilSun,
  cilLanguage,
  cifGb,
  cifPl,
  cilBookmark,
} from '@coreui/icons';

import { AppBreadcrumb } from './index';
import {
  AppHeaderDropdown,
} from './header';
import {
  cilHome,
  cilCarAlt,
  cilInfo,
  cilEnvelopeOpen,
  cilSearch,
  cilUser,
  cilUserPlus
} from '@coreui/icons';
import type { RootState } from './../store';

const AppHeader = () => {
  const headerRef = useRef<HTMLDivElement>(null);
  const { colorMode, setColorMode } = useColorModes('coreui-pro-react-admin-template-theme-light');
  const { i18n, t } = useTranslation();

  const dispatch = useDispatch();
  const asideShow = useSelector((state: RootState) => state.ui.asideShow);
  const sidebarShow = useSelector((state: RootState) => state.ui.sidebarShow);
  const user = useSelector((state: RootState) => state.auth.user);

  useEffect(() => {
    document.addEventListener('scroll', () => {
      headerRef.current &&
        headerRef.current.classList.toggle('shadow-sm', document.documentElement.scrollTop > 0);
    });
  }, []);

  return (
    <CHeader position="sticky" className="mb-4 p-0" ref={headerRef}>
      <CContainer className="border-bottom px-4" fluid>
      <CHeaderNav className="d-none d-md-flex">
          <CNavItem>
            <CNavLink as={NavLink} to="/trips" className="nav-link">
              <CIcon icon={cilCarAlt} className="me-2" />
              {t('Trips')}
            </CNavLink>
          </CNavItem>
          <CNavItem>
            <CNavLink as={NavLink} to="/about" className="nav-link">
              <CIcon icon={cilInfo} className="me-2" />
              {t('About Us')}
            </CNavLink>
          </CNavItem>
          <CNavItem>
            <CNavLink as={NavLink} to="/contact" className="nav-link">
              <CIcon icon={cilEnvelopeOpen} className="me-2" />
              {t('Contact Us')}
            </CNavLink>
          </CNavItem>
          <CNavItem>
            <CNavLink as={NavLink} to="/search-booking" className="nav-link">
              <CIcon icon={cilSearch} className="me-2" />
              {t('Search Booking by PNR')}
            </CNavLink>
          </CNavItem>
          {user && (
          <CNavItem>
            <CNavLink as={NavLink} to="/" className="nav-link">
              <CIcon icon={cilBookmark} className="me-2" />
              {t('My Bookings')}
            </CNavLink>
          </CNavItem>
          )}
        </CHeaderNav>
        <CHeaderNav className="d-none d-md-flex ms-auto">
        </CHeaderNav>
        <CHeaderNav className="ms-auto ms-md-0">
        {!user ? (
            <>
              <CButton 
                color="primary" 
                className="me-2" 
                as={NavLink} 
                to="/login"
              >
                <CIcon icon={cilUser} className="me-2" />
                Login
              </CButton>
              <CButton 
                color="light" 
                className="me-2" 
                as={NavLink} 
                to="/register"
              >
                <CIcon icon={cilUserPlus} className="me-2" />
                Register
              </CButton>
            </>
          ) : (
            <AppHeaderDropdown />
          )}
          <li className="nav-item py-1">
            <div className="vr h-100 mx-2 text-body text-opacity-75"></div>
          </li>
          <CDropdown variant="nav-item" placement="bottom-end">
            <CDropdownToggle caret={false}>
              <CIcon icon={cilLanguage} size="lg" />
            </CDropdownToggle>
            <CDropdownMenu>
              <CDropdownItem
                active={i18n.language === 'en'}
                className="d-flex align-items-center"
                as="button"
                onClick={() => i18n.changeLanguage('en')}
              >
                <CIcon className="me-2" icon={cifGb} size="lg" /> English
              </CDropdownItem>
              <CDropdownItem
                active={i18n.language === 'tr'}
                className="d-flex align-items-center"
                as="button"
                onClick={() => i18n.changeLanguage('tr')}
              >
                <CIcon className="me-2" icon={cifPl} size="lg" /> Turkish
              </CDropdownItem>
            </CDropdownMenu>
          </CDropdown>
          <CDropdown variant="nav-item" placement="bottom-end">
            <CDropdownToggle caret={false}>
              {colorMode === 'dark' ? (
                <CIcon icon={cilMoon} size="lg" />
              ) : colorMode === 'auto' ? (
                <CIcon icon={cilContrast} size="lg" />
              ) : (
                <CIcon icon={cilSun} size="lg" />
              )}
            </CDropdownToggle>
            <CDropdownMenu>
              <CDropdownItem
                active={colorMode === 'light'}
                className="d-flex align-items-center"
                as="button"
                type="button"
                onClick={() => setColorMode('light')}
              >
                <CIcon className="me-2" icon={cilSun} size="lg" /> {t('light')}
              </CDropdownItem>
              <CDropdownItem
                active={colorMode === 'dark'}
                className="d-flex align-items-center"
                as="button"
                type="button"
                onClick={() => setColorMode('dark')}
              >
                <CIcon className="me-2" icon={cilMoon} size="lg" /> {t('dark')}
              </CDropdownItem>
              <CDropdownItem
                active={colorMode === 'auto'}
                className="d-flex align-items-center"
                as="button"
                type="button"
                onClick={() => setColorMode('auto')}
              >
                <CIcon className="me-2" icon={cilContrast} size="lg" /> Auto
              </CDropdownItem>
            </CDropdownMenu>
          </CDropdown>
          <li className="nav-item py-1">
            <div className="vr h-100 mx-2 text-body text-opacity-75"></div>
          </li>
          
          {user && <AppHeaderDropdown />}
        </CHeaderNav>
        <CHeaderToggler
          onClick={() => dispatch({ type: 'ui/setUIState', payload: { asideShow: !asideShow } })}
          style={{ marginInlineEnd: '-12px' }}
        >
          <CIcon icon={cilApplicationsSettings} size="sm" />
        </CHeaderToggler>
      </CContainer>
      <CContainer className="px-4" fluid>
        <AppBreadcrumb />
      </CContainer>
    </CHeader>
  );
};

export default AppHeader;
