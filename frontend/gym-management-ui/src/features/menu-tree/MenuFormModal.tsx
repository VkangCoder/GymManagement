import { Form, Input, Modal } from 'antd';
import { useEffect } from 'react';
import type { MenuFormModalState } from './types';

interface MenuFormValues {
  title: string;
}

interface MenuFormModalProps {
  state: MenuFormModalState;
  onClose: () => void;
  onSubmit: (title: string) => void;
}

export default function MenuFormModal({ state, onClose, onSubmit }: MenuFormModalProps) {
  const [form] = Form.useForm<MenuFormValues>();
  const isEdit = state?.mode === 'edit';

  useEffect(() => {
    if (state === null) return;
    form.setFieldsValue({ title: isEdit ? state.node.title : '' });
  }, [state, isEdit, form]);

  const handleOk = async () => {
    const values = await form.validateFields();
    onSubmit(values.title.trim());
  };

  return (
    <Modal
      open={state !== null}
      title={isEdit ? 'Sửa mục menu' : 'Thêm mục menu'}
      okText={isEdit ? 'Cập nhật' : 'Thêm'}
      cancelText="Hủy"
      onOk={handleOk}
      onCancel={onClose}
      destroyOnHidden
    >
      <Form form={form} layout="vertical" preserve={false}>
        <Form.Item
          name="title"
          label="Tên mục menu"
          rules={[
            { required: true, message: 'Vui lòng nhập tên mục menu' },
            { min: 2, message: 'Tên phải có ít nhất 2 ký tự' },
          ]}
        >
          <Input placeholder="Nhập tên mục menu" autoFocus />
        </Form.Item>
      </Form>
    </Modal>
  );
}
