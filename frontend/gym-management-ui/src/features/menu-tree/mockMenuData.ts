import type { MenuTreeNode } from './types';

// TODO: replace with a real GET /api/menu-tree call once the backend endpoint exists.
export const initialMenuData: MenuTreeNode[] = [
  {
    key: 'dashboard',
    title: 'Tổng quan',
    parentKey: null,
    icon: 'dashboard',
  },
  {
    key: 'members',
    title: 'Quản lý hội viên',
    parentKey: null,
    icon: 'members',
    children: [
      { key: 'members-list', title: 'Danh sách hội viên', parentKey: 'members' },
      { key: 'members-packages', title: 'Gói tập', parentKey: 'members' },
    ],
  },
  {
    key: 'coaches',
    title: 'Quản lý huấn luyện viên',
    parentKey: null,
    icon: 'coaches',
    children: [
      { key: 'coaches-list', title: 'Danh sách HLV', parentKey: 'coaches' },
      { key: 'coaches-schedule', title: 'Lịch huấn luyện', parentKey: 'coaches' },
    ],
  },
  {
    key: 'equipment',
    title: 'Quản lý thiết bị',
    parentKey: null,
    icon: 'equipment',
    children: [
      { key: 'equipment-list', title: 'Danh sách thiết bị', parentKey: 'equipment' },
      { key: 'equipment-brand', title: 'Thương hiệu', parentKey: 'equipment' },
    ],
  },
  {
    key: 'settings',
    title: 'Cài đặt hệ thống',
    parentKey: null,
    icon: 'settings',
  },
];
