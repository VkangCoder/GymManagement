import {
  AppstoreOutlined,
  DashboardOutlined,
  SettingOutlined,
  ToolOutlined,
  TeamOutlined,
  UserSwitchOutlined,
} from '@ant-design/icons';
import type { ReactNode } from 'react';

export type MenuIconKey = 'dashboard' | 'members' | 'coaches' | 'equipment' | 'settings';

export const menuIconMap: Record<MenuIconKey, ReactNode> = {
  dashboard: <DashboardOutlined />,
  members: <TeamOutlined />,
  coaches: <UserSwitchOutlined />,
  equipment: <ToolOutlined />,
  settings: <SettingOutlined />,
};

export const defaultMenuIcon: ReactNode = <AppstoreOutlined />;

export function resolveMenuIcon(icon: MenuIconKey | undefined): ReactNode {
  return icon ? menuIconMap[icon] : defaultMenuIcon;
}
