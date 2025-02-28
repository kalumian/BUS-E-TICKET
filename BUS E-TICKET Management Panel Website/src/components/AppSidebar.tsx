import { NavLink } from 'react-router-dom'
import { useSelector, useDispatch } from 'react-redux'

import {
  CCloseButton,
  CSidebar,
  CSidebarBrand,
  CSidebarFooter,
  CSidebarHeader,
  CSidebarToggler,
} from '@coreui/react-pro'
import CIcon from '@coreui/icons-react'

import { AppSidebarNav } from './AppSidebarNav'

import logo from './../assets/brand/logo.png'
import { sygnet } from './../assets/brand/sygnet'

// sidebar nav config
import navigation from '../_nav'
import { RootState, setUIState } from 'src/store'

const AppSidebar = () => {
  const dispatch = useDispatch()
  const StateOfUI = useSelector((state: RootState) => state.ui)

  return (
    <CSidebar
      className="border-end"
      colorScheme="light"
      position="fixed"
      unfoldable={StateOfUI.sidebarUnfoldable}
      visible={StateOfUI.sidebarShow}
      onVisibleChange={(visible) => {
        dispatch(setUIState({  sidebarShow: visible }))
      }}
    >
      <CSidebarHeader className="border-bottom">
        <CSidebarBrand as={NavLink} to="/">
            <img src={logo} alt="Logo" style={{ height: '60px' }} />
        </CSidebarBrand>
        <CCloseButton
          className="d-lg-none"
          dark
          onClick={() => dispatch(setUIState({sidebarShow: false }))}
        />
      </CSidebarHeader>
      <AppSidebarNav items={navigation} />
      <CSidebarFooter className="border-top d-none d-lg-flex">
        <CSidebarToggler
          onClick={() => dispatch(setUIState({sidebarUnfoldable: !StateOfUI.sidebarUnfoldable }))}
        />
      </CSidebarFooter>
    </CSidebar>
  )
}

export default AppSidebar
